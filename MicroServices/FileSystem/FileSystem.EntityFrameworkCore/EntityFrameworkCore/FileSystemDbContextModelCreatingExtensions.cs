using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace FileSystem.EntityFrameworkCore
{
    public static class FileSystemDbContextModelCreatingExtensions
    {
        public static void ConfigureFileSystem(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
        }
    }
}
