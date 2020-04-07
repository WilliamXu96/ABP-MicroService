using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Business.EntityFrameworkCore
{
    public class BusinessMigrationDbContextFactory: IDesignTimeDbContextFactory<BusinessMigrationDbContext>
    {
        public BusinessMigrationDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<BusinessMigrationDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Business"));

            return new BusinessMigrationDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
