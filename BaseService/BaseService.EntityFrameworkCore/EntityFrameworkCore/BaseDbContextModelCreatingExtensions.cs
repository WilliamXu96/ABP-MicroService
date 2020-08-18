using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace BaseService.EntityFrameworkCore
{
    public static class BaseDbContextModelCreatingExtensions
    {
        public static void ConfigureBaseService(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
        }
    }
}
