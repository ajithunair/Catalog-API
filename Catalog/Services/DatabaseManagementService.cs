using Catalog.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Services
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialization(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ItemContext>().Database.Migrate();
            }
        }
    }
}