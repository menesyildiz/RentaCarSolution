using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentaCar.Entities;

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
                opts.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; database=RentaCarDb; trusted_connection=true");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=CarList}/{id?}");

            app.Run();
        }
    }
}