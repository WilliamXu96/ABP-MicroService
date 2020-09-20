using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.EntityFrameworkCore
{
    public class FileSystemMigrationDbContextFactory : IDesignTimeDbContextFactory<FileSystemMigrationDbContext>
    {
        public FileSystemMigrationDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<FileSystemMigrationDbContext>()
                .UseSqlServer(configuration.GetConnectionString("FileSystem"));

            return new FileSystemMigrationDbContext(builder.Options);
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
