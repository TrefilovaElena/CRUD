using CRUD.DAL;
using CRUD.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace CRUD.API.StartupExtensions
{
    public static class EFExtensions
    {
        public static void RegisterEntityFramework(this WebApplicationBuilder builder)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
                builder.SetMinimumLevel(LogLevel.Information);
            });
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
            builder.Services.AddDbContext<AppDbContext>(options =>
                     options.UseNpgsql(connectionString, o =>
                           o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
        }

        public static async Task AppendDatabaseMigrationAsync(this WebApplication application)
        {
            using var scope = application.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await dbContext.Database.MigrateAsync();

        }
    }
}
