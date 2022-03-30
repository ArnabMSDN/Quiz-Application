using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quiz_Application.Services;
using Quiz_Application.Web.Authentication;

namespace Quiz_Application.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // NET6 startup (without Startup.cs)
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Configuration, builder.Services);

            var app = builder.Build();
            ConfigureMiddleware(app, app.Environment);
            ConfigureEndpoints(app);

            app.Run();
        }


        // Add services to the container.
        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            var connectionString = configuration.GetConnectionString("QuizDBConnection");
            services.AddDbContext<QuizDBContext>(options => options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // Add services to the container.
            services.AddServices();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllersWithViews()/*+++.AddRazorRuntimeCompilation()*/;
            services.AddSingleton<BasicAuthentication>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }

        private static void ConfigureMiddleware(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
                        {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            } else
                        {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
        }

        private static void ConfigureEndpoints(IEndpointRouteBuilder app)
        {
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }

    }
}
