using Application.Exceptions;
using Domain.Enums;
using FileService.Application.Contracts;
using FileService.Application.DTOs.MediaFiles;
using FileService.Application.Features.MediaFiles;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using System.Drawing;
using Utilities;
using Utilities.Files;

namespace FileService.Infrastructure.Services.Files
{
    /// <summary>
    /// مدیریت فایل ها
    /// </summary>
    public class FileHelper : IFileHelper
    {
        #region Constants
        private const string FILE_DIR = "Files";
        private const string Pic_Base_DIR = "Pictures";
        private const string Pic_LARGE_DIR = "Large";
        private const string Pic_THUMB_DIR = "Thumb";
        #endregion


        #region Constructors
        protected IConfiguration _config { get; }
        protected ILogger<FileHelper> _logger { get; }
        protected string _absolutePath { get; }

        public FileHelper(IConfiguration config, ILogger<FileHelper> logger)
        {
            _config = config;
            _logger = logger;
            _absolutePath = InitialUoloadAbsolutePath();
        }
        #endregion



        #region مقدار دهی و مشخص کردن مسیر پایه ذخیره سازی فایل ها
        public string InitialUoloadAbsolutePath()
        {
            try
            {
                var dir = _config.GetSection("UploadAbsolutePath").Value;
                if (string.IsNullOrEmpty(dir))
                    dir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                return dir;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ایجاد مسیر مطلق ذخیره سازی فایل.");
                throw new FileException("ایجاد مسیر مطلق آپلود فایل با خطا همراه بوده است!");
            }

        }
        #endregion



        #region فایل ها

        #region ذخیره فایل روی هارد و گرفتن خروجی مورد نظر
        public async Task<MediaFileUploadResultDTO> SaveFile(IFormFile file, MediaFileGroup group)
        {
            try
            {
                #region تولید نام فایل
                var extension = Path.GetExtension(file.FileName);
                var guid = Guid.NewGuid().ToString().GetUrlFriendly();
                var title = Path.GetFileNameWithoutExtension(file.FileName);
                string fileName = guid + "_" + title.GetUrlFriendly() + extension;
                #endregion

                #region ایجاد مسیر نسبی ذخیره فایل
                var dir = GetFilePath(group);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                #endregion

                #region بررسی تکراری نبودن نام فایل در مسیر ایجاد شده
                var fullPath = Path.Combine(dir, fileName);
                int i = 1;
                var temp = fileName;
                while (File.Exists(fullPath))
                {
                    fileName = i + "-" + temp;
                    fullPath = Path.Combine(dir, fileName);
                    i++;
                }
                #endregion

                #region ذخیره فایل در هارد
                using (var fs = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                #endregion


                string getType = MimeMapping.GetMimeMapping(fullPath);
                return new MediaFileUploadResultDTO(title, fileName, file.Length, fullPath, group, getType, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"خطا در ذخیره سازی فایل | group: {group.ToString()}");
                throw new FileException("ذخیره فایل با خطا همراه بوده است!");

            }
        }
        #endregion


        #region تولید آدرس فایل
        public string GetFilePath(MediaFileGroup group, string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
                return Path.Combine(_absolutePath, group.ToString(), FILE_DIR);
            else
                return Path.Combine(_absolutePath, group.ToString(), FILE_DIR, fileName);
        }
        #endregion


        #region حذف فایل
        public async Task DeleteFile(string fileName, MediaFileGroup group)
        {
            await Task.Run(() =>
            {
                var fullPath = GetFilePath(group, fileName);
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
                else
                {
                    _logger.LogWarning($"فایل \"{fileName}\" یافت نشد!");
                    //throw new FileNotFoundException("فایل مورد نظر یافت نشد!");
                }
            });
        }
        #endregion
        #endregion



        #region تصاویر

        #region ذخیره تصویر روی هارد و گرفتن خروجی مورد نظر
        public async Task<MediaFileUploadResultDTO> SavePic(
            IFormFile file, MediaFileGroup group,
            int LargeMaxWidth, int LargeMaxHeight,
            int ThumbMaxWidth, int ThumbMaxHeight)
        {
            try
            {
                #region بررسی تصویر بودن فایل
                if (!file.IsImage())
                {
                    _logger.LogWarning($"آپلود فایل غیر مجاز در بخش تصاویر | نام فایل {file.FileName} | group: {group.ToString()}");
                    throw new FileException("در این فرم تنها بارگذاری تصویر مجاز است!");
                }
                #endregion

                #region تولید نام فایل
                var extension = Path.GetExtension(file.FileName);
                var guid = Guid.NewGuid().ToString().GetUrlFriendly();
                var title = Path.GetFileNameWithoutExtension(file.FileName);
                string fileName = guid + "_" + title.GetUrlFriendly() + extension;
                #endregion

                #region ایجاد مسیر نسبی ذخیره تصویر
                var largePath = GetPicLargePath(group);
                var thumbPath = GetPicThumbPath(group);

                if (!Directory.Exists(largePath))
                    Directory.CreateDirectory(largePath);
                if (!Directory.Exists(thumbPath))
                    Directory.CreateDirectory(thumbPath);
                #endregion

                #region بررسی تکراری نبودن نام تصویر در مسیر ایجاد شده
                var fullPath = Path.Combine(largePath, fileName);
                int i = 1;
                var temp = fileName;
                while (File.Exists(fullPath))
                {
                    fileName = i + "-" + temp;
                    fullPath = Path.Combine(largePath, fileName);
                    i++;
                }
                #endregion

                #region ذخیره تصویر در هارد
                largePath = Path.Combine(largePath, fileName);
                thumbPath = Path.Combine(thumbPath, fileName);
                using (var fs = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                using (var fs = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                #endregion

                #region ریسایز کردن تصاویر
                await Resize(thumbPath, thumbPath, ThumbMaxWidth, ThumbMaxHeight);
                await Resize(largePath, largePath, LargeMaxWidth, LargeMaxHeight);
                #endregion

                string getType = MimeMapping.GetMimeMapping(fullPath);
                return new MediaFileUploadResultDTO(title, fileName, file.Length, fullPath, group, getType, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ایجاد مسیر نسبی ذخیره سازی فایل.");
                throw new FileException("ایجاد مسیر نسبی آپلود فایل با خطا همراه بوده است!");

            }
        }
        #endregion


        #region تولید آدرس عکس
        public string GetPicThumbPath(MediaFileGroup group, string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
                return Path.Combine(_absolutePath, group.ToString(), Pic_Base_DIR, Pic_THUMB_DIR);
            else
                return Path.Combine(_absolutePath, group.ToString(), Pic_Base_DIR, Pic_THUMB_DIR, fileName);
        }

        public string GetPicLargePath(MediaFileGroup group, string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
                return Path.Combine(_absolutePath, group.ToString(), Pic_Base_DIR, Pic_LARGE_DIR);
            else
                return Path.Combine(_absolutePath, group.ToString(), Pic_Base_DIR, Pic_LARGE_DIR, fileName);
        }
        #endregion


        #region حذف عکس
        public async Task DeletePic(string fileName, MediaFileGroup group)
        {
            await Task.Run(() =>
            {
                #region حذف تصویر بزرگ
                string largePath = GetPicLargePath(group, fileName);
                if (File.Exists(largePath))
                    File.Delete(largePath);
                else
                {
                    _logger.LogWarning($"تصویر کوچک \"{largePath}\" یافت نشد!");
                    //throw new FileNotFoundException("فایل مورد نظر یافت نشد!");
                }
                #endregion

                #region حذف تصویر کوچک
                string thumbPath = GetPicThumbPath(group, fileName);
                if (File.Exists(thumbPath))
                    File.Delete(thumbPath);
                else
                {
                    _logger.LogWarning($"تصویر کوچک \"{thumbPath}\" یافت نشد!");
                    //throw new FileNotFoundException("فایل مورد نظر یافت نشد!");
                }
                #endregion
            });
        }
        #endregion



        #region ریسایز کردن تصویر
        #region ریسایز کردن تصویر با SkiaSharp و MagickNET
        /// <summary>
        ///  ریسایز کردن تصویر با SkiaSharp و MagickNET
        /// </summary>
        /// <param name="sourcePath">آدرس کامل عکسی که قرار است ریسایز شود</param>
        /// <param name="destPath">آدرس مقصد عکس ریسایز شده</param>
        /// <param name="maxWidth">عرض عکس</param>
        /// <param name="maxHeight">طول عکس</param>
        /// <returns></returns>
        private async Task Resize(string sourcePath, string destPath, int maxWidth, int maxHeight)
        {
            // Create an 80x80 thumbnail, and possibly resize the original if it exceeds an arbitrary max width.
            var extension = Path.GetExtension(sourcePath).ToLower();

            #region ریسایز کردن تصویر گیف با MagickNET
            if (extension.IsGif())
            {
                #region محاسبه طول و عرض تبدیل شده با توجه به ماکزیمم مقادیر
                var sourceSize = GetGifImageSize(sourcePath);
                var destSize = new Size(maxWidth, maxHeight);
                if (sourceSize != Size.Empty)
                    destSize = CalcSize(sourceSize, destSize);
                #endregion

                #region ریسایز تصاویر
                using (var thumbStream = MagickNetResizeImage(sourcePath, destSize.Width, destSize.Height))
                {
                    using (var thumbFileStream = File.OpenWrite(destPath))
                    {
                        await thumbStream.CopyToAsync(thumbFileStream);
                    }
                }
                #endregion
            }
            #endregion

            #region ریسایز کردن باقی تصاویر بجز گیف با SkiaSharp
            else
            {
                #region محاسبه طول و عرض تبدیل شده با توجه به ماکزیمم مقادیر
                var sourceSize = GetNotGifImageSize(sourcePath);
                var destSize = new Size(maxWidth, maxHeight);
                if (sourceSize != Size.Empty)
                    destSize = CalcSize(sourceSize, destSize);
                #endregion

                #region ریسایز تصویر
                using (var thumbStream = SkiaSharpResizeImage(sourcePath, destSize.Width, destSize.Height))
                {
                    using (var thumbFileStream = File.OpenWrite(destPath))
                    {
                        await thumbStream.CopyToAsync(thumbFileStream);
                    }
                }
                #endregion
            }
            #endregion

        }
        #endregion


        #region کراپ کردن تصویر با SkiaSharp و MagickNET
        /// <summary>
        /// ریسایز کردن تصویر
        /// </summary>
        /// <param name="sourcePath">آدرس کامل عکسی که قرار است ریسایز شود</param>
        /// <param name="destPath">آدرس مقصد عکس ریسایز شده</param>
        /// <param name="maxWidth">عرض عکس</param>
        /// <param name="maxHeight">طول عکس</param>
        /// <returns></returns>
        private async Task Crop(string sourcePath, string destPath, int maxWidth, int maxHeight)
        {
            #region ریسایز کردن تصویر
            // For GIFs, we have to use MagickNET. For JPEGs and PNGs, we can use SkiaSharp.
            var extension = Path.GetExtension(sourcePath).ToLower();

            #region ریسایز کردن تصویر گیف با MagickNET
            if (extension.IsGif())
            {
                using (var thumbStream = MagickNetResizeImage(sourcePath, maxWidth, maxHeight))
                {
                    using (var thumbFileStream = File.OpenWrite(destPath))
                    {
                        await thumbStream.CopyToAsync(thumbFileStream);
                    }
                }
            }
            #endregion

            #region ریسایز کردن باقی تصاویر بجز گیف با SkiaSharp
            else
            {
                using (var thumbStream = SkiaSharpResizeImage(sourcePath, maxWidth, maxHeight))
                {
                    using (var thumbFileStream = File.OpenWrite(destPath))
                    {
                        await thumbStream.CopyToAsync(thumbFileStream);
                    }
                }
            }
            #endregion
            #endregion
        }
        #endregion



        #region ریسایز کردن باقی تصاویر بجز گیف با SkiaSharp
        private MemoryStream SkiaSharpResizeImage(string sourcePath, int destWidthPx, int destHeightPx)
        {
            try
            {
                var originalBmp = SKBitmap.Decode(sourcePath);
                var scaledBmp = originalBmp.Resize(new SKImageInfo(destWidthPx, destHeightPx), SKFilterQuality.High);
                var scaledImg = SKImage.FromBitmap(scaledBmp);

                SKEncodedImageFormat encodedImageFormat;
                int quality;

                var extension = Path.GetExtension(sourcePath).ToLower();
                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg":
                        encodedImageFormat = SKEncodedImageFormat.Jpeg;
                        quality = 90;
                        break;

                    case ".png":
                        encodedImageFormat = SKEncodedImageFormat.Png;
                        quality = 100;
                        break;

                    // Skia doesn't support resizing GIFs. We use Magick.NET for that.
                    //case ".gif":
                    //    break;

                    default:
                        _logger.LogWarning($"تصویر قابل ریسایز کردن نیست. فرمت تصویر بارگذاری شده : \"{extension}\"");
                        throw new FileException($"تصویر قابل ریسایز کردن نیست. فرمت تصویر بارگذاری شده : \"{extension}\"");
                }

                var scaledImgData = scaledImg.Encode(encodedImageFormat, quality);

                var thumbnailStream = new MemoryStream();
                scaledImgData.SaveTo(thumbnailStream);
                thumbnailStream.Position = 0L;

                return thumbnailStream;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "خطا در ریسایز کردن تصویر");
                throw new FileException($"خطا در ریسایز کردن تصویر");
            }
        }
        #endregion



        #region ریسایز کردن تصویر گیف با MagickNET
        private MemoryStream MagickNetResizeImage(string sourcePath, int destWidthPx, int destHeightPx)
        {
            try
            {
                // Read from file
                using (var collection = new MagickImageCollection(sourcePath))
                {
                    // This will remove the optimization and change the image to how it looks at that point
                    // during the animation. More info here: http://www.imagemagick.org/Usage/anim_basics/#coalesce
                    collection.Coalesce();

                    // Resize each image in the collection to a width of {maxwidth}. When zero is specified for the height
                    // the height will be calculated with the aspect ratio.
                    foreach (MagickImage image in collection)
                    {
                        image.Resize(width: destWidthPx, height: destHeightPx);
                    }

                    // Save the result
                    var resizedImgStream = new MemoryStream();
                    collection.Write(resizedImgStream);
                    resizedImgStream.Position = 0L;

                    return resizedImgStream;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "خطا در ریسایز کردن تصویر");
                throw new FileException($"خطا در ریسایز کردن تصویر");
            }
        }
        #endregion



        #region گرفتن طول و عرض تصاویر غیر از GIF
        public Size GetNotGifImageSize(string sourcePath)
        {
            var originalImage = SKBitmap.Decode(sourcePath);
            if (originalImage != null)
                return new Size(originalImage.Width, originalImage.Height);

            return Size.Empty;
        }
        #endregion


        #region گرفتن طول و عرض تصاویر GIF
        public Size GetGifImageSize(string sourcePath)
        {
            using var collection = new MagickImageCollection(sourcePath);
            collection.Coalesce();
            if (collection.FirstOrDefault() != null)
            {
                var sourceWidth = collection.First().Width;
                var sourceHeight = collection.First().Width;
                return new Size(sourceWidth, sourceHeight);
            }
            return Size.Empty;
        }
        #endregion


        #region محاسبه سایز جدید بر اساس ماکزیمم مقادیر برای ریسایز تصاویر
        public Size CalcSize(Size sourceSize, Size maxSize)
        {
            #region اگر طول و عرض تصویر از طول و عرض مورد نیاز کمتر بود نیازی به ریسایز نیست
            if (maxSize.Width > sourceSize.Width && maxSize.Height > sourceSize.Height)
                return sourceSize;
            #endregion

            int sourceWidth = sourceSize.Width;
            int sourceHeight = sourceSize.Height;
            float nPercentW = maxSize.Width / (float)sourceWidth;
            float nPercentH = maxSize.Height / (float)sourceHeight;
            float nPercent = Math.Min(nPercentH, nPercentW);

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            return new Size(destWidth, destHeight);
        }
        #endregion


        #endregion
        #endregion



        #region فایل یا تصویر مورد نظر وجود دارد؟
        public async Task<bool> IsExist(string fileName, MediaFileGroup group, bool IsPic)
        {
            return await Task.Run(() =>
            {
                string fullPath = null;
                if (IsPic)
                    fullPath = GetPicLargePath(group, fileName);
                else
                    fullPath = GetFilePath(group, fileName);

                if (string.IsNullOrEmpty(fullPath))
                    return false;
                return File.Exists(fullPath);
            });
        }
        #endregion

    }
}
