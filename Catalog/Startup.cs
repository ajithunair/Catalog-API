using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Catalog
{
    public class Startup
    {
        private string ConnectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("CatalogDB");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var server = Configuration["DbServer"] ?? "ms-sql-server";
            var port = Configuration["DbPort"] ?? "1433";
            var password = Configuration["Password"] ?? "Pa55w0rd!";
            var database = Configuration["Database"] ?? "CatalogDB";
            ConnectionString = $"server={server},{port};Initial Catalog={database};User ID=SA;Password={password}";

            services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
            services.AddDbContext<ItemContext>(options => options.UseSqlServer(ConnectionString));
            //services.AddSingleton<IItemsRepository, InMemItemsRepository>();
            services.AddScoped<IItemsRepository, SqlItemsRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog v1"));
                app.UseHttpsRedirection();
            }
            
            app.UseRouting();

            app.UseAuthorization();
            Services.DatabaseManagementService.MigrationInitialization(app);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
