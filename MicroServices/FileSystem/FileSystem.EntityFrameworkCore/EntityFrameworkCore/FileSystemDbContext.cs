using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace FileSystem.EntityFrameworkCore
{
    [ConnectionStringName("FileSystem")]
    public class FileSystemDbContext : AbpDbContext<FileSystemDbContext>
    {
        public FileSystemDbContext(DbContextOptions<FileSystemDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureFileSystem();
        }
    }
}
