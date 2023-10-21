using Base.Application.Common.IdentityConfigs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using Utilities;
using static Application.IdentityConfigs.IdentityScopes.AuthService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.IdentityConfigs
{
    /// <summary>
    /// اسکپ های دسترسی برای آیدنتیتی سرور
    /// </summary>
    public static class IdentityScopes
    {
        private static Type type = typeof(IdentityScopes);


        #region دسترسی ها
        [Description("همه منوها")]
        public const string Full = "Full"; 
        #endregion


        #region سرویس های احراز هویت و کاربران
        public static class AuthService
        {
            private static Type type = typeof(AuthService);
            private const string prefix = "AuthService";

            [Description("سرویس های احراز هویت و کاربران")]
            public const string Full = $"{prefix}.Full";


            #region مدیریت کاربران
            public static class Users
            {
                private static Type type = typeof(Users);
                private const string prefix = $"{AuthService.prefix}.Users";

                #region دسترسی ها
                [Description("مدیریت کاربران")]
                public const string Full = $"{prefix}.Full";

                [Description("لیست کاربران")]
                public const string Read = $"{prefix}.Read";

                [Description("افزودن کاربر")]
                public const string Create = $"{prefix}.Create";

                [Description("ویرایش کاربر")]
                public const string Update = $"{prefix}.Update";

                [Description("حذف کاربر")]
                public const string Delete = $"{prefix}.Delete";
                #endregion



                #region توابع
                #region اسکوپ ها
                public static IEnumerable<IdentityScopeDTO> GetScopes()
                    => IdentityBaseMethods.GetScopes(type);
                #endregion

                #region کلایم ها
                /// <summary>
                /// گرفتن لیست مقدار همه اسکوپ ها
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<string> GetClaims()
                    => IdentityBaseMethods.GetClaims(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت فلت
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsFlat()
                    => IdentityBaseMethods.GetClaimsFlat(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت درختی
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsTree()
                    => IdentityBaseMethods.GetClaimsTree(type, Full);

                #endregion
                #endregion

            }
            #endregion




            #region مدیریت نقش ها
            public static class Roles
            {
                private static Type type = typeof(Roles);
                private const string prefix = $"{AuthService.prefix}.Roles";

                #region دسترسی ها
                [Description("مدیریت نقش ها")]
                public const string Full = $"{prefix}.Full";

                [Description("لیست نقش ها")]
                public const string Read = $"{prefix}.Read";

                [Description("افزودن نقش")]
                public const string Create = $"{prefix}.Create";

                [Description("ویرایش نقش")]
                public const string Update = $"{prefix}.Update";

                [Description("حذف نقش")]
                public const string Delete = $"{prefix}.Delete";
                #endregion



                #region توابع
                #region اسکوپ ها
                public static IEnumerable<IdentityScopeDTO> GetScopes()
                    => IdentityBaseMethods.GetScopes(type);
                #endregion

                #region کلایم ها
                /// <summary>
                /// گرفتن لیست مقدار همه اسکوپ ها
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<string> GetClaims()
                    => IdentityBaseMethods.GetClaims(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت فلت
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsFlat()
                    => IdentityBaseMethods.GetClaimsFlat(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت درختی
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsTree()
                    => IdentityBaseMethods.GetClaimsTree(type, Full);

                #endregion
                #endregion

            }
            #endregion




            #region مدیریت منو ها
            public static class Menus
            {
                private static Type type = typeof(Menus);
                private const string prefix = $"{AuthService.prefix}.Menus";

                #region دسترسی ها
                [Description("لیست منو ها")]
                public const string Full = $"{prefix}.Full"; 
                #endregion



                #region توابع
                #region اسکوپ ها
                public static IEnumerable<IdentityScopeDTO> GetScopes()
                    => IdentityBaseMethods.GetScopes(type);
                #endregion

                #region کلایم ها
                /// <summary>
                /// گرفتن لیست مقدار همه اسکوپ ها
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<string> GetClaims()
                    => IdentityBaseMethods.GetClaims(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت فلت
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsFlat()
                    => IdentityBaseMethods.GetClaimsFlat(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت درختی
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsTree()
                    => IdentityBaseMethods.GetClaimsTree(type, Full);

                #endregion
                #endregion

            }
            #endregion



            #region توابع
            #region ریسورس ها
            /// <summary>
            /// گرفتن لیست همه ریسورس ها و سرویس ها
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<IdentityResourceDTO> GetResources()
                => IdentityBaseMethods.GetResources(type);
            #endregion

            #region اسکوپ ها
            public static IEnumerable<IdentityScopeDTO> GetScopes()
                => IdentityBaseMethods.GetScopes(type);
            #endregion

            #region کلایم ها
            /// <summary>
            /// گرفتن لیست مقدار همه اسکوپ ها
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<string> GetClaims()
                => IdentityBaseMethods.GetClaims(type);


            /// <summary>
            /// گرفتن لیست همه اسکوپ ها بصورت فلت
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<IdentityClaimDTO> GetClaimsFlat()
                => IdentityBaseMethods.GetClaimsFlat(type);


            /// <summary>
            /// گرفتن لیست همه اسکوپ ها بصورت درختی
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<IdentityClaimDTO> GetClaimsTree()
                => IdentityBaseMethods.GetClaimsTree(type, Full);

            #endregion
            #endregion
        }
        #endregion



        #region سرویس های کد گذاری کالا
        public static class Coding
        {
            private static Type type = typeof(Coding);
            private const string prefix = "CodingService";

            [Description("سرویس های کد گذاری کالا")]
            public const string Full = $"{prefix}.Full";


            #region مدیریت  نام پایه ها 
            public static class BaseNames
            {
                private static Type type = typeof(BaseNames);
                private const string prefix = $"{Coding.prefix}.BaseNames";

                #region دسترسی ها
                [Description("مدیریت  نام پایه ها")]
                public const string Full = $"{prefix}.Full";

                [Description("لیست نام پایه ها")]
                public const string Read = $"{prefix}.Read";

                [Description("افزودن نام پایه")]
                public const string Create = $"{prefix}.Create";

                [Description("ویرایش نام پایه")]
                public const string Update = $"{prefix}.Update";

                [Description("حذف نام پایه")]
                public const string Delete = $"{prefix}.Delete";

                [Description("لیست فایلهای نام پایه ")]
                public const string ReadFile = $"{prefix}.ReadFile";

                [Description("افزودن فایل های نام پایه")]
                public const string CreateFile = $"{prefix}.CreateFile";

                [Description("ویرایش عناوین فایل های نام پایه")]
                public const string UpdateFile = $"{prefix}.UpdateFile";

                [Description("حذف فایل های نام پایه")]
                public const string DeleteFile = $"{prefix}.DeleteFile";
                #endregion



                #region توابع
                #region اسکوپ ها
                public static IEnumerable<IdentityScopeDTO> GetScopes()
                    => IdentityBaseMethods.GetScopes(type);
                #endregion

                #region کلایم ها
                /// <summary>
                /// گرفتن لیست مقدار همه اسکوپ ها
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<string> GetClaims()
                    => IdentityBaseMethods.GetClaims(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت فلت
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsFlat()
                    => IdentityBaseMethods.GetClaimsFlat(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت درختی
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsTree()
                    => IdentityBaseMethods.GetClaimsTree(type, Full);

                #endregion
                #endregion

            }
            #endregion

            #region مدیریت  واحدها 
            public static class Units
            {
                private static Type type = typeof(Units);
                private const string prefix = $"{Coding.prefix}.Units";

                #region دسترسی ها
                [Description("مدیریت  واحدها")]
                public const string Full = $"{prefix}.Full";

                [Description("لیست واحدها")]
                public const string Read = $"{prefix}.Read";

                [Description("افزودن واحد")]
                public const string Create = $"{prefix}.Create";

                [Description("ویرایش واحد")]
                public const string Update = $"{prefix}.Update";

                [Description("حذف واحد")]
                public const string Delete = $"{prefix}.Delete";

                [Description("لیست فایلهای واحد ")]
                public const string ReadFile = $"{prefix}.ReadFile";

                [Description("افزودن فایل های واحد")]
                public const string CreateFile = $"{prefix}.CreateFile";

                [Description("ویرایش عناوین فایل های واحد")]
                public const string UpdateFile = $"{prefix}.UpdateFile";

                [Description("حذف فایل های واحد")]
                public const string DeleteFile = $"{prefix}.DeleteFile";
                #endregion



                #region توابع
                #region اسکوپ ها
                public static IEnumerable<IdentityScopeDTO> GetScopes()
                    => IdentityBaseMethods.GetScopes(type);
                #endregion

                #region کلایم ها
                /// <summary>
                /// گرفتن لیست مقدار همه اسکوپ ها
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<string> GetClaims()
                    => IdentityBaseMethods.GetClaims(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت فلت
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsFlat()
                    => IdentityBaseMethods.GetClaimsFlat(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت درختی
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsTree()
                    => IdentityBaseMethods.GetClaimsTree(type, Full);

                #endregion
                #endregion

            }
            #endregion

            #region مدیریت ارزش ها 
            public static class Worths
            {
                private static Type type = typeof(Worths);
                private const string prefix = $"{Coding.prefix}.Worths";

                #region دسترسی ها
                [Description("مدیریت ارزش ها")]
                public const string Full = $"{prefix}.Full";

                [Description("لیست ارزش ها")]
                public const string Read = $"{prefix}.Read";

                [Description("افزودن ارزش")]
                public const string CreateWorth = $"{prefix}.CreateWorth";

                [Description("افزودن ارزش معادل")]
                public const string CreateWorthDictionary = $"{prefix}.CreateWorthDictionary";

                [Description("حذف ارزش")]
                public const string Delete = $"{prefix}.Delete";

                [Description("لیست فایلهای ارزش ")]
                public const string ReadFile = $"{prefix}.ReadFile";

                [Description("افزودن فایل های ارزش")]
                public const string CreateFile = $"{prefix}.CreateFile";

                [Description("ویرایش عناوین فایل های ارزش")]
                public const string UpdateFile = $"{prefix}.UpdateFile";

                [Description("حذف فایل های ارزش")]
                public const string DeleteFile = $"{prefix}.DeleteFile";
                #endregion



                #region توابع
                #region اسکوپ ها
                public static IEnumerable<IdentityScopeDTO> GetScopes()
                    => IdentityBaseMethods.GetScopes(type);
                #endregion

                #region کلایم ها
                /// <summary>
                /// گرفتن لیست مقدار همه اسکوپ ها
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<string> GetClaims()
                    => IdentityBaseMethods.GetClaims(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت فلت
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsFlat()
                    => IdentityBaseMethods.GetClaimsFlat(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت درختی
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsTree()
                    => IdentityBaseMethods.GetClaimsTree(type, Full);

                #endregion
                #endregion

            }
            #endregion

            #region مدیریت  مشخصه ها 
            public static class Properties
            {
                private static Type type = typeof(Properties);
                private const string prefix = $"{Coding.prefix}.Properties";

                #region دسترسی ها
                [Description("مدیریت  مشخصه ها")]
                public const string Full = $"{prefix}.Full";

                [Description("لیست  مشخصه ها")]
                public const string Read = $"{prefix}.Read";

                [Description("افزودن  مشخصه")]
                public const string Create = $"{prefix}.Create";

                [Description("ویرایش  مشخصه")]
                public const string Update = $"{prefix}.Update";

                [Description("حذف  مشخصه")]
                public const string Delete = $"{prefix}.Delete";

                [Description("لیست فایلهای  مشخصه ")]
                public const string ReadFile = $"{prefix}.ReadFile";

                [Description("افزودن فایل های  مشخصه")]
                public const string CreateFile = $"{prefix}.CreateFile";

                [Description("ویرایش عناوین فایل های  مشخصه")]
                public const string UpdateFile = $"{prefix}.UpdateFile";

                [Description("حذف فایل های  مشخصه")]
                public const string DeleteFile = $"{prefix}.DeleteFile";
                #endregion



                #region توابع
                #region اسکوپ ها
                public static IEnumerable<IdentityScopeDTO> GetScopes()
                    => IdentityBaseMethods.GetScopes(type);
                #endregion

                #region کلایم ها
                /// <summary>
                /// گرفتن لیست مقدار همه اسکوپ ها
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<string> GetClaims()
                    => IdentityBaseMethods.GetClaims(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت فلت
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsFlat()
                    => IdentityBaseMethods.GetClaimsFlat(type);


                /// <summary>
                /// گرفتن لیست همه اسکوپ ها بصورت درختی
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<IdentityClaimDTO> GetClaimsTree()
                    => IdentityBaseMethods.GetClaimsTree(type, Full);

                #endregion
                #endregion

            }
            #endregion

            #region توابع
            #region ریسورس ها
            /// <summary>
            /// گرفتن لیست همه ریسورس ها و سرویس ها
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<IdentityResourceDTO> GetResources()
                => IdentityBaseMethods.GetResources(type);
            #endregion

            #region اسکوپ ها
            public static IEnumerable<IdentityScopeDTO> GetScopes()
                => IdentityBaseMethods.GetScopes(type);
            #endregion

            #region کلایم ها
            /// <summary>
            /// گرفتن لیست مقدار همه اسکوپ ها
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<string> GetClaims()
                => IdentityBaseMethods.GetClaims(type);


            /// <summary>
            /// گرفتن لیست همه اسکوپ ها بصورت فلت
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<IdentityClaimDTO> GetClaimsFlat()
                => IdentityBaseMethods.GetClaimsFlat(type);


            /// <summary>
            /// گرفتن لیست همه اسکوپ ها بصورت درختی
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<IdentityClaimDTO> GetClaimsTree()
                => IdentityBaseMethods.GetClaimsTree(type, Full);

            #endregion
            #endregion
        }
        #endregion


        #region توابع
        #region ریسورس ها
        /// <summary>
        /// گرفتن لیست همه ریسورس ها و سرویس ها
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResourceDTO> GetResources()
            => IdentityBaseMethods.GetResources(type);
        #endregion

        #region اسکوپ ها
        public static IEnumerable<IdentityScopeDTO> GetScopes()
            => IdentityBaseMethods.GetScopes(type);
        #endregion

        #region کلایم ها
        /// <summary>
        /// گرفتن لیست مقدار همه اسکوپ ها
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetClaims()
            => IdentityBaseMethods.GetClaims(type);


        /// <summary>
        /// گرفتن لیست همه اسکوپ ها بصورت فلت
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityClaimDTO> GetClaimsFlat()
            => IdentityBaseMethods.GetClaimsFlat(type);


        /// <summary>
        /// گرفتن لیست همه اسکوپ ها بصورت درختی
        /// </summary>
        /// <returns></returns>
        public static IdentityClaimDTO GetClaimsTree()
            => IdentityBaseMethods.GetClaimsTree(type, Full).FirstOrDefault();
        
        #endregion
        #endregion                
    }


}
