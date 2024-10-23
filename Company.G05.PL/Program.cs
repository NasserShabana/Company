using AutoMapper;
using Company.G05.BLL.Interfaces;
using Company.G05.BLL.Repositories;
using Company.G05.DAL.Data.Contexts;
using Company.G05.DAL.Models;
using Company.G05.PL.Controllers;
using Company.G05.PL.Mapping.Employees;
using Company.G05.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.G05.PL
{
    public class Program
    {
        public static void Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.ConfigureApplicationCookie(Config => Config.LoginPath = "/Account/SignIn");

            //builder.Services.AddScoped<AppDbContext>(); //Allow DI For AppDbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<DepartmentController>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepositrory>();
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(config => {


                config.LoginPath = "/Account/SignIn";
            });         

            builder.Services.AddScoped <IScopedServices, ScopedServices>(); // Per Request
            builder.Services.AddTransient<ITransientServices , TransientServices>(); //Per Operation 
            builder.Services.AddSingleton<ISingeltonServices, SingeltonServices>(); //Per App

            var app = builder.Build();

            // Update database

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<AppDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                 context.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "An error occurred while migrating the database.");
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
