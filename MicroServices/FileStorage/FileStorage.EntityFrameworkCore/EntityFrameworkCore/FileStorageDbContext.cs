using FileStorage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace FileStorage.EntityFrameworkCore
{
    [ConnectionStringName("FileStorage")]
    public class FileStorageDbContext : AbpDbContext<FileStorageDbContext>
    {
        public DbSet<FileInfo> FileInfos { get; set; }

        public FileStorageDbContext(DbContextOptions<FileStorageDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureFileStorage();
        }
    }
}
