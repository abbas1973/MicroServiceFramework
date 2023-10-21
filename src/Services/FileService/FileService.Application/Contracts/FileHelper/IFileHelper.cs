using Domain.Enums;
using FileService.Application.DTOs.MediaFiles;
using Microsoft.AspNetCore.Http;

namespace FileService.Application.Contracts
{
    /// <summary>
    /// مدیریت فایل ها
    /// </summary>
    public interface IFileHelper
    {
        #region فایل ها
        /// <summary>
        /// ذخیره فایل روی هارد و تهیه خروجی مورد نیاز
        /// </summary>
        /// <param name="file"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<MediaFileUploadResultDTO> SaveFile(IFormFile file, MediaFileGroup group);


        /// <summary>
        /// تولید آدرس فایل
        /// </summary>
        /// <param name="group">دسته بندی فایل</param>
        /// <param name="fileName">نام فایل</param>
        /// <returns></returns>
        string GetFilePath(MediaFileGroup group, string fileName = null);

        /// <summary>
        /// حذف فایل
        /// </summary>
        /// <param name="fileName">نام فایل</param>
        /// <param name="group">دسته بندی فایل</param>
        /// <returns></returns>
        Task DeleteFile(string fileName, MediaFileGroup group);

        #endregion




        #region تصویر ها
        /// <summary>
        /// ذخیره تصویر روی هارد و تهیه خروجی مورد نیاز
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<MediaFileUploadResultDTO> SavePic(
            IFormFile file, MediaFileGroup group,
            int LargeMaxWidth, int LargeMaxHeight,
            int ThumbMaxWidth, int ThumbMaxHeight);


        /// <summary>
        /// تولید آدرس کوچگ تصویر
        /// </summary>
        /// <param name="group">دسته بندی تصویر</param>
        /// <param name="fileName">نام تصویر</param>
        /// <returns></returns>
        string GetPicThumbPath(MediaFileGroup group, string fileName = null);

        /// <summary>
        /// تولید آدرس بزرگ تصویر
        /// </summary>
        /// <param name="group">دسته بندی تصویر</param>
        /// <param name="fileName">نام تصویر</param>
        /// <returns></returns>
        string GetPicLargePath(MediaFileGroup group, string fileName = null);


        /// <summary>
        /// حذف تصویر
        /// </summary>
        /// <param name="picName">نام تصویر</param>
        /// <param name="group">دسته بندی تصویر</param>
        /// <returns></returns>
        Task DeletePic(string picName, MediaFileGroup group);

        #endregion




        #region فایل یا تصویر مورد نظر وجود دارد؟
        Task<bool> IsExist(string fileName, MediaFileGroup group, bool IsPic);
        #endregion
    }
}
