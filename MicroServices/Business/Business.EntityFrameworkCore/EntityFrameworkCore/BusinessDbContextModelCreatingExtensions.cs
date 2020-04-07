using Business.BaseData;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Business.EntityFrameworkCore
{
    public static class BusinessDbContextModelCreatingExtensions
    {
        public static void ConfigureBusiness(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<DataDictionary>(b =>
            {
                b.ToTable("dictionary");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();

                b.Property(x => x.Name).IsRequired().HasMaxLength(BusinessConsts.MaxNameLength);
                b.Property(x => x.Code).HasMaxLength(BusinessConsts.MaxCodeLength);
                b.Property(x => x.FullName).IsRequired().HasMaxLength(BusinessConsts.MaxFullNameLength);
                b.Property(x => x.Notes).HasMaxLength(BusinessConsts.MaxNotesLength);
                b.Property(x => x.SEQ).IsRequired();
                b.Property(x => x.IsDeleted).HasDefaultValue(false);

                b.HasIndex(q => q.Code);
            });
        }
    }
}
