using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OSD.STS.StsConfigurations;

namespace OSD.STS
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();


            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;


            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                                        //.AddInMemoryClients(new List<Client>())
                                        //.AddInMemoryIdentityResources(new List<IdentityResource>())
                                        //.AddInMemoryApiResources(new List<ApiResource>())
                                        //.AddAspNetIdentity<IdentityUser>()
                                        //        .AddOperationalStore(options =>
                                        //                    options.ConfigureDbContext = builder =>
                                        //                        builder.UseSqlServer(_config[DbConfig.ConnectionStringKeyIdp.Replace("__", ":")],
                                        //                            sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
                                        //        .AddConfigurationStore(options =>
                                        //                    options.ConfigureDbContext = builder =>
                                        //                        builder.UseSqlServer(_config[DbConfig.ConnectionStringKeyIdp.Replace("__", ":")],
                                        //                            sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

                                        //services.AddDbContextPool<ApplicationDbContext>(options =>
                                        //                options.UseSqlServer(_config[DbConfig.ConnectionStringKeyIdpUser.Replace("__", ":")]));

                                        .AddOperationalStore(options =>
                                options.ConfigureDbContext = builder =>
                                    builder.UseSqlServer(_config.GetConnectionString("IdentityDbConnectionString"),
                                        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
                    .AddConfigurationStore(options =>
                                options.ConfigureDbContext = builder =>
                                    builder.UseSqlServer(_config.GetConnectionString("IdentityDbConnectionString"),
                                        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

            services.AddDbContextPool<ApplicationDbContext>(options =>
                            options.UseSqlServer(_config.GetConnectionString("IdentityDbConnectionString"), sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();




            //services.AddIdentity<IdentityUser, IdentityRole>()/*.AddEntityFrameworkStores<ApplicationDbContext>().AddClaimsPrincipalFactory<IUserClaimsPrincipalFactory<ApplicationDbContext>>()*/
            //.AddDefaultTokenProviders();
            //        services.AddIdentity<IdentityUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //        services.AddDbContextPool<ApplicationDbContext>(builder =>
            //                    builder.UseSqlServer(_config.GetConnectionString("IdentityDbConnectionString")));
            //                            //sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));



            //services.AddDbContext<ApplicationDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
