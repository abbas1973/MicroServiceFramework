using Application.IdentityConfigs;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Mappers;
using AuthServer.Domain.Entities;

namespace AuthServer.Infrastructure.Persistence.Identity.Configs
{
    public static class SeedData
    {

        #region مایگریت کردن و مقدار دهی اولیه دیتابیس ها
        public static void SeedDatabases(this IApplicationBuilder app)
        {
            app.InitApplicationDb();
            app.InitIdentityServerDb();
        } 
        #endregion



        #region مقدار دهی اولیه به دیتابیس اپلیکیشن و آیدنتیتی
        public static void InitApplicationDb(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                applicationContext.Database.Migrate();

                if (!applicationContext.Users.Any())
                {
                    #region کاربر ادمین
                    var admin = new User
                    {
                        UserName = "admin",
                        NormalizedUserName = "ADMIN",
                        PasswordHash = "AQAAAAEAACcQAAAAEGda4GqSfLZo341LgVaY/jMQx5cnCBbrSFJP5u0iqUPysXn8+zNd5/esAsZzUUGOBw==", // @Am123123
                        Name = "ادمین",
                        CreateDate = DateTime.Now,
                        SecurityStamp = "ZQE3Q2TZUUPWBCP3K4VJWIHDXKKAYQ4X",
                        PhoneNumber = "09359785415",
                        PhoneNumberConfirmed = true,
                        ConcurrencyStamp = "351bd5e3-d651-4720-9c81-96324601e4d4",
                        LockoutEnabled = true,
                        IsEnabled = true,
                        UserRoles = new List<UserRole>()
                    };
                    #endregion


                    #region نقش ادمین
                    var adminRole = new Role
                    {
                        Name = "ادمین",
                        NormalizedName = "ادمین",
                        ConcurrencyStamp = "1fc462ac-b7a8-4e2f-8344-e3b1a4be4b1f",
                        CreateDate = DateTime.Now,
                        IsEnabled = true
                    };
                    #endregion

                    #region اضافه کردن نقش ادمین به کاربر ادمین
                    admin.UserRoles = new List<UserRole>()
                    {
                        new UserRole()
                        {
                            Role = adminRole
                        }
                    }; 
                    #endregion

                    applicationContext.Add(admin);
                }
            }
        } 
        #endregion



        #region مقدار دهی اولیه به دیتابیس آیدنتیتی سرور
        public static void InitIdentityServerDb(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                #region migrate databases
                var PersistedContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                PersistedContext.Database.Migrate();

                var ConfigurationContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                ConfigurationContext.Database.Migrate();
                #endregion



                #region seedData
                var _scopes = IdentityScopes.GetScopes();
                var _resources = IdentityScopes.GetResources();

                #region add clients
                if (!ConfigurationContext.Clients.Any())
                {
                    #region Pardco

                    var pardcoClient = new IdentityServer4.EntityFramework.Entities.Client()
                    {
                        ClientId = "pardco",
                        ClientName = "pardco general client",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword.Select(
                            x => new IdentityServer4.EntityFramework.Entities.ClientGrantType()
                            {
                                GrantType = x
                            }).ToList(),
                        ClientSecrets = new List<IdentityServer4.EntityFramework.Entities.ClientSecret>()
                        {
                            new IdentityServer4.EntityFramework.Entities.ClientSecret()
                            {
                                Value = "1qaz@WSX".Sha256()
                            }
                        },
                        AllowOfflineAccess = true, // برای گرفتن رفرش توکن
                        AccessTokenLifetime = 1800, // طول عمر اکسس توکن به ثانیه معادل نیم ساعت
                        RefreshTokenUsage = 1, // از هر رفرش توکن چند ساعت میتوان استفاده کرد
                        AbsoluteRefreshTokenLifetime = 2592000,
                        SlidingRefreshTokenLifetime = 1296000,
                        AlwaysIncludeUserClaimsInIdToken = true, // برای اضافه کردن کلایم های کاربر
                        AlwaysSendClientClaims = true, // برای ارسال کلایم های کلاینت
                        UpdateAccessTokenClaimsOnRefresh = true,// آپدیت کردن کلایم های اکسس توکن هنگام رفرش
                    };

                    #region اسکوپ های مجاز
                    pardcoClient.AllowedScopes = new List<IdentityServer4.EntityFramework.Entities.ClientScope>()
                        {
                            new IdentityServer4.EntityFramework.Entities.ClientScope() {
                                Scope = IdentityScopes.Full
                            }
                        };
                    //pardcoClient.AllowedScopes.AddRange(_scopes.Select(
                    //    x => new IdentityServer4.EntityFramework.Entities.ClientScope()
                    //    {
                    //        Scope = x.Scope
                    //    }
                    //    ).ToList());
                    #endregion

                    ConfigurationContext.Clients.Add(pardcoClient);
                    #endregion
                }
                #endregion


                #region add api resources
                if (!ConfigurationContext.ApiResources.Any())
                {
                    foreach (var item in _resources)
                    {
                        var r = new IdentityServer4.EntityFramework.Entities.ApiResource()
                        {
                            Name = item.Resource,
                            DisplayName = item.Description,
                            Scopes = item.Scopes.Select(x => new IdentityServer4.EntityFramework.Entities.ApiResourceScope()
                            {
                                Scope = x
                            }).ToList(),
                            UserClaims = item.Claims.Select(x => new IdentityServer4.EntityFramework.Entities.ApiResourceClaim() 
                            { 
                                Type = x
                            }).ToList()
                        };
                        ConfigurationContext.ApiResources.Add(r);
                    }
                }
                #endregion


                #region add identity resources
                if (!ConfigurationContext.IdentityResources.Any())
                {
                    var identityResources = _resources.Select(x => new IdentityServer4.EntityFramework.Entities.IdentityResource() { 
                        Name = x.Resource,
                        DisplayName = x.Description,
                        UserClaims = x.Claims.Select(z => new IdentityServer4.EntityFramework.Entities.IdentityResourceClaim
                        {
                            Type = z
                        }).ToList()
                    });
                }
                #endregion


                #region add scopes
                if (!ConfigurationContext.ApiScopes.Any())
                {
                    var scopes = new List<IdentityServer4.EntityFramework.Entities.ApiScope>();

                    #region افزودن اسکوپ فول
                    scopes.Add(new IdentityServer4.EntityFramework.Entities.ApiScope()
                    {
                        Name = IdentityScopes.Full,
                        DisplayName = "دسترسی به همه سرویس ها",
                        UserClaims = new List<IdentityServer4.EntityFramework.Entities.ApiScopeClaim> {
                        new IdentityServer4.EntityFramework.Entities.ApiScopeClaim
                        {
                            Type = IdentityScopes.Full
                        }}
                    });
                    #endregion

                    #region افزودن بقیه اسکوپ ها
                    var tmp = _scopes.Select(x => new IdentityServer4.EntityFramework.Entities.ApiScope()
                    {
                        Name = x.Scope,
                        DisplayName = x.Description,
                        UserClaims = x.Claims.Select(z => new IdentityServer4.EntityFramework.Entities.ApiScopeClaim
                        {
                            Type = z
                        }).ToList()
                    });
                    scopes.AddRange(tmp);
                    #endregion
                    ConfigurationContext.ApiScopes.AddRange(scopes);
                }
                #endregion 
                #endregion

                ConfigurationContext.SaveChanges();
            }

        }
        #endregion

    }
}
