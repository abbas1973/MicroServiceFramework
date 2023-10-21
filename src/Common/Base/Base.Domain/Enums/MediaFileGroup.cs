using System.ComponentModel;

namespace Domain.Enums
{
    /// <summary>
    /// دسته بندی فایل که نشان میدهد فایل برای چه جدولی
    /// اپلود شده است. مثلا برای محصولات یا پست ها
    /// </summary>
    public enum MediaFileGroup
    {
        [Description("سایر")]
        Other = 0,

        [Description("نام پایه")]
        BaseName = 1,

        [Description("واحد")]
        Unit = 2,

        [Description("ارزش")]
        Wotrth = 3,

        [Description("مشخصه")]
        Property = 4
    }
}
