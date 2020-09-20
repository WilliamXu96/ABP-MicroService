using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace FileSystem.EntityFrameworkCore
{
    public class FileSystemMigrationDbContext : AbpDbContext<FileSystemMigrationDbContext>
    {
        public FileSystemMigrationDbContext(DbContextOptions<FileSystemMigrationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureFileSystem();
        }
    }
}
