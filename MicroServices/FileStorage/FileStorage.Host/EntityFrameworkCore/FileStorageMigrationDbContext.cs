using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace FileStorage.EntityFrameworkCore
{
    public class FileStorageMigrationDbContext : AbpDbContext<FileStorageMigrationDbContext>
    {
        public FileStorageMigrationDbContext(DbContextOptions<FileStorageMigrationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureFileStorage();
        }
    }
}
