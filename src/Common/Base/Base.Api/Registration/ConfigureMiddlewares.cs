using Application.Middleware;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Base.Api.Registration
{
    public static class ConfigureMiddlewares
    {
        /// <summary>
        /// استفاده از میدلور های کاستوم توسط app
        /// IApplicationBuilder
        /// </summary>
        public static IApplicationBuilder UseCustomMiddlewares(this WebApplication app, string serviceNameFa)
        {
            app.UseCors("CorsPolicy");

            app.UseErrorHandlerMiddleware();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.routeprefix = "docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", serviceNameFa);
                c.DocExpansion(DocExpansion.None);
            });
            #endregion


            app.UseHttpsRedirection();


            #region امنیت
            #region تنظیم هدر برای جلوگیری از حملات
            app.Use(async (context, next) =>
            {
                // برای جلوگیری از iframe شدن صفحات سایت و براي مقابله در برابر حملات ClickJacking
                context.Response.Headers.Add("X-Frame-Options", "DENY");

                // جلوگیری از حملات xss
                context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");

                // جلوگیری از MIME-Sniffing و تغییر پسوند فایل ها
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

                // جلوگیری از باز کردن فایل های خارج از حالت لوکال
                // اگر لینکی به سایتی مثل جی کوئری داشتیم باید اینجا اضافه کنیم.
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval'; font-src 'self' https://fonts.gstatic.com; connect-src 'self' wss:");

                context.Response.Headers.Remove("Server");
                await next();
            });
            #endregion
            #endregion


            app.UseCheckUserVerifiedMiddleware();


            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
