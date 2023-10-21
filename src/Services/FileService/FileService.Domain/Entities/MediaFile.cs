using Domain.Entities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FileService.Domain.Entities
{
    /// <summary>
    /// فایل های ذخیره شده در مسیر هایی با فرمت زیر
    /// <para>
    /// برای فایل:
    /// {absolutePath}/{GroupName}/MediaFile/{fileName}
    /// </para>
    /// <para>
    /// برای تصویر:
    /// {absolutePath}/{GroupName}/Picture/Large/{fileName}
    /// {absolutePath}/{GroupName}/Picture/Thumb/{fileName}
    /// </para>
    /// </summary>
    public class MediaFile : BaseEntity
    {
        [Display(Name = "عنوان فارسی")]
        public string TitleFa { get; set; }

        [Display(Name = "عنوان لاتین")]
        public string TitleEn { get; set; }

        /// <summary>
        /// نام فایل به همراه پسوند بدون مسیر
        /// </summary>
        [Display(Name = "نام فایل")]
        public string FileName { get; set; }


        /// <summary>
        /// حجم فایل بر حسب بایت
        /// </summary>
        [Display(Name = "حجم فایل")]
        public long Size { get; set; }

        /// <summary>
        /// نشان میدهد که فایل آپلود شده 
        /// به عوان تصویر آپلود شده است یا یک فایل
        /// </summary>
        [Display(Name = "تصویر است یا فایل؟")]
        public bool IsPic { get; set; }

        /// <summary>
        /// نوع فایل: تصویر، ویدیو، پی دی اف و ...
        /// </summary>
        [Display(Name = "نوع فایل")]
        public FileFormat Format { get; set; }


        /// <summary>
        /// دسته بندی فایل که نشان میدهد فایل برای چه جدولی
        /// اپلود شده است. مثلا برای محصولات یا پست ها
        /// </summary>
        [Display(Name = "دسته بندی فایل")]
        public MediaFileGroup Group { get; set; }

    }
}
