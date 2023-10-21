using Application.IdentityConfigs;
using System.Reflection;
using Utilities;

namespace Base.Application.Common.IdentityConfigs
{
    /// <summary>
    /// توابع پایه استفاده شده برای محاسبه ریسورس، اسکوپ و کلایم ها
    /// </summary>
    public static class IdentityBaseMethods
    {
        #region ریسورس ها
        public static IEnumerable<IdentityResourceDTO> GetResources(this Type type)
        {
            var model = new List<IdentityResourceDTO>();

            #region گرفتن ریسورس ها از نوع کلاس فعلی
            var full = type.GetConstantsWithDescription(propNames: nameof(IdentityScopes.Full));
            if (full != null && full.Any() && full.First().Key != IdentityScopes.Full)
            {
                var resource = new IdentityResourceDTO
                {
                    Resource = full.First().Key.Replace(".Full" , ""),
                    Description = full.First().Value,
                    Scopes = new List<string>(),
                    Claims = new List<string>()
                };

                #region اسکوپ های زیر مجموعه
                MethodInfo scopeMethodInfo = type.GetMethod(nameof(IdentityScopes.GetScopes));
                if (scopeMethodInfo != null)
                {
                    var res = scopeMethodInfo.Invoke(null, null);
                    if (res != null)
                    {
                        var scopes = res as List<IdentityScopeDTO>;
                        resource.Scopes = scopes?.Select(x => x.Scope);
                    }
                }
                #endregion

                #region کلایم های زیر مجموعه
                MethodInfo claimMethodInfo = type.GetMethod(nameof(IdentityScopes.GetClaims));
                if (claimMethodInfo != null)
                {
                    var res = claimMethodInfo.Invoke(null, null);
                    if (res != null)
                        resource.Claims = res as List<string>;
                }
                #endregion

                model.Add(resource);
            }
            #endregion

            #region گرفتن ریسورس از کلاس های زیر مجموعه
            var resources = type.InvokeMethod<IdentityResourceDTO>(nameof(GetResources));
            model.AddRange(resources); 
            #endregion

            return model;
        }
        #endregion


        #region اسکوپ ها
        public static IEnumerable<IdentityScopeDTO> GetScopes(this Type type)
        {
            var model = new List<IdentityScopeDTO>();

            #region گرفتن اسکوپ ها از کلاس های زیر مجموعه
            var scopes = type.InvokeMethod<IdentityScopeDTO>(nameof(GetScopes));
            model.AddRange(scopes);
            if (scopes != null && scopes.Any())
                return model;
            #endregion

            #region گرفتن اسکوپ ها از نوع کلاس فعلی
            var full = type.GetConstantsWithDescription(propNames: nameof(IdentityScopes.Full));
            if (full != null && full.Any())
            {
                var scope = new IdentityScopeDTO
                {
                    Scope = full.First().Key.Replace(".Full", ""),
                    Description = full.First().Value,
                    Claims = new List<string>()
                };

                #region کلایم های زیر مجموعه
                MethodInfo claimMethodInfo = type.GetMethod(nameof(IdentityScopes.GetClaims));
                if (claimMethodInfo != null)
                {
                    var res = claimMethodInfo.Invoke(null, null);
                    if (res != null)
                        scope.Claims = res as List<string>;
                }
                #endregion

                model.Add(scope);
            }
            #endregion

            return model;
        }
        #endregion


        #region کلایم ها
        #region گرفتن لیست مقدار همه کلایم ها
        public static IEnumerable<string> GetClaims(this Type type)
        {
            var model = type.GetConstantValues();
            var claims = type.InvokeMethod<string>(nameof(GetClaims));
            model.AddRange(claims);
            return model;
        }
        #endregion



        #region گرفتن لیست همه کلایم ها بصورت فلت
        public static IEnumerable<IdentityClaimDTO> GetClaimsFlat(this Type type)
        {
            var model = type.GetConstantsWithDescription()
                .Select(x => new IdentityClaimDTO(x.Key, x.Value)).ToList();

            var claims = type.InvokeMethod<IdentityClaimDTO>(nameof(GetClaimsFlat));
            model.AddRange(claims);
            return model;
        }
        #endregion



        #region گرفتن لیست همه کلایم ها بصورت درختی
        public static IEnumerable<IdentityClaimDTO> GetClaimsTree(this Type type, string rootClaim)
        {
            #region root
            var Claims = type.GetConstantsWithDescription()
                            .Select(x => new IdentityClaimDTO(x.Key, x.Value));
            var root = Claims.Single(x => x.Claim == rootClaim);
            #endregion

            #region sub Claims
            var subClaims = new List<IdentityClaimDTO>();
            subClaims.AddRange(Claims.Where(x => x.Claim != rootClaim));

            var claims = type.InvokeMethod<IdentityClaimDTO>(nameof(GetClaimsTree));
            subClaims.AddRange(claims);

            root.SubClaims = subClaims;
            #endregion

            return new List<IdentityClaimDTO> { root };
        }
        #endregion
        #endregion


        #region فراخوانی یک متد خاص از درون پروپرتی های 
        public static List<Tout> InvokeMethod<Tout>(this Type Tin, string methodName) where Tout : class
        {
            var model = new List<Tout>();
            var nesteds = Tin.GetNestedTypes(BindingFlags.Public);
            foreach (var prop in nesteds)
            {
                MethodInfo methodInfo = prop.GetMethod(methodName);
                if (methodInfo != null)
                {
                    var res = methodInfo.Invoke(null, null);
                    if (res != null)
                    {
                        var val = res as List<Tout>;
                        model.AddRange(val);
                    }
                }
            }
            return model;
        }
        #endregion
    }
}
