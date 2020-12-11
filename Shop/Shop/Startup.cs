using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //добавляем поддержку котроллеров и представлений (MVC)
            services.AddControllersWithViews()
                //выставляем совместимость с asp.net core 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //очень важно соблюдать такой порядок регистрации middleware

            //в процессе разработки важно видеть подробную информацию об ошибках
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //подключение поддержки статичных файлов в приложении(css, js и т.д.)
            app.UseStaticFiles();

            //регистрация нужных нам маршрутов (эндпоинтов)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute("default", "{controller-Home}/{action-Index}/{id?}");
            //});
        }
    }
}
