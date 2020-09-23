using FileStorage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace FileStorage.EntityFrameworkCore
{
    public static class FileStorageDbContextModelCreatingExtensions
    {
        public static void ConfigureFileStorage(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<FileInfo>(b =>
            {
                b.ToTable("file_info");

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(64);
                b.Property(x => x.RealName).IsRequired().HasMaxLength(64);
                b.Property(x => x.Size).IsRequired().HasMaxLength(100);
                b.Property(x => x.Suffix).IsRequired().HasMaxLength(50);
                b.Property(x => x.Md5Code).IsRequired().HasMaxLength(256);
                b.Property(x => x.Path).IsRequired().HasMaxLength(256);
                b.Property(x => x.Url).IsRequired().HasMaxLength(256);
            });
        }
    }
}
