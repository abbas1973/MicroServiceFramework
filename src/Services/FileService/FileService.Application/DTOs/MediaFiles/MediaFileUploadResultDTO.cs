using Domain.Enums;
using Utilities.Files;
using FileFormat = Utilities.Files.FileFormat;

namespace FileService.Application.DTOs.MediaFiles
{
    public class MediaFileUploadResultDTO
    {

        #region Constructors
        public MediaFileUploadResultDTO(
            string title, string fileName, long fileSize,
            string fullPath, MediaFileGroup group, string type,
            bool isPicture)
        {
            Title = title;
            Name = fileName;
            Size = fileSize;
            Type = type;
            Format = fileName.GetFormat();
            Group = group;
            DeleteType = "POST";
            DeleteUrl = fileName.GetDeleteUrl(group.ToString(), isPicture);
            Url = fileName.GetDownloadUrl(group.ToString(), isPicture, false);
            if (isPicture)
                ThumbnailUrl = fileName.GetStreamUrl(group.ToString(), true, true);
        }
        #endregion


        #region Properties
        /// <summary>
        /// عنوان تصویر آپلود شده
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// نام تصویر
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// سایز تصویر به بایت
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// نوع فایل
        /// <para>
        /// مثلا: 
        /// application/octet-stream
        /// </para>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// آدرس مستقیم دانلود فایل روی سرور
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// آدرس حذف فایل
        /// </summary>
        public string DeleteUrl { get; set; }

        /// <summary>
        /// نوع متد حذف تصویر
        /// <para>
        /// POST / GET
        /// </para>
        /// </summary>
        public string DeleteType { get; set; } = "POST";

        /// <summary>
        /// آدرس پیش نمایش فایل برای تصاویر
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// فرمت فایل
        /// </summary>
        public FileFormat Format { get; set; }
        public string FormatSt => Format.ToString();

        /// <summary>
        /// دسته بندی فایل
        /// </summary>
        public MediaFileGroup Group { get; set; }
        public string GroupSt => Group.ToString(); 
        #endregion
    }
}
