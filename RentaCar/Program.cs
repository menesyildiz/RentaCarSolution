using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RentaCar.Entities;
using RentaCar.Managers;

namespace RentaCar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DatabaseContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IBrandManager, BrandManager>();
            builder.Services.AddScoped<IModelManager, ModelManager>();
            builder.Services.AddScoped<ICustomerManager, CustomerManager>();
            builder.Services.AddScoped<IHomeManager, HomeManager>();
            builder.Services.AddScoped<IAccountManager, AccountManager>();

            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.Cookie.Name = "rentacar.auth";
                    opts.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    opts.LoginPath = "/Account/Login";
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}