using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Application.AutoMapper;
using NerdStore.Catalogo.Data;
using Nerdstore.Vendas.Data;
using NerdStore.WebApp.MVC.Setup;
using NerdStore.WebApp.MVC.Data;
using Microsoft.OpenApi.Models;
using NerdStore.WebApp.MVC.Models;
using System.Reflection;
using System.Globalization;

namespace NerdStore.WebApp.MVC.Configurations
{
    public static class WebAppConfigurations
    {
        public static void AddConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDbContext<CatalogoContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDbContext<VendasContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(
                options => {
                    options.SignIn.RequireConfirmedAccount = false;
                }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.RegisterServices();

            services.AddEndpointsApiExplorer();
                       
        }

        public static void UseConfigureServices(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Configure the HTTP request pipeline.
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
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

        }
    }

}
