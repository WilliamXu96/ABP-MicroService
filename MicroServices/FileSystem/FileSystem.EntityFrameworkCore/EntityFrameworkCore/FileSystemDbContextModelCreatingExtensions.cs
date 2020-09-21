using FileSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace FileSystem.EntityFrameworkCore
{
    public static class FileSystemDbContextModelCreatingExtensions
    {
        public static void ConfigureFileSystem(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<FileInfo>(b =>
            {
                b.ToTable("file_info");

                b.ConfigureByConvention();

                b.Property(x => x.FileName).IsRequired().HasMaxLength(50);
                b.Property(x => x.Md5Code).IsRequired().HasMaxLength(256);
                b.Property(x => x.Url).IsRequired().HasMaxLength(256);
            });
        }
    }
}
