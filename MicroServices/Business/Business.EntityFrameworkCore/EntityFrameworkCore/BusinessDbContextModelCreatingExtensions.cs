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
                b.ToTable("base_dict");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();

                b.Property(x => x.Name).IsRequired().HasMaxLength(BusinessConsts.MaxNameLength);
                b.Property(x => x.Description).HasMaxLength(BusinessConsts.MaxNotesLength);
                b.Property(x => x.IsDeleted).HasDefaultValue(false);

                b.HasIndex(q => q.Name);
            });

            builder.Entity<DataDictionaryDetail>(b =>
            {
                b.ToTable("base_dict_details");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();

                b.Property(x => x.Label).IsRequired().HasMaxLength(BusinessConsts.MaxNameLength);
                b.Property(x => x.Value).IsRequired().HasMaxLength(BusinessConsts.MaxNotesLength);
                b.Property(x => x.IsDeleted).HasDefaultValue(false);

                b.HasIndex(q => q.Pid);
            });

            builder.Entity<Organization>(b =>
            {
                b.ToTable("base_orgs");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();
                b.ConfigureSoftDelete();

                b.Property(x => x.Name).IsRequired().HasMaxLength(BusinessConsts.MaxNameLength);
                b.Property(x => x.FullName).IsRequired().HasMaxLength(BusinessConsts.MaxFullNameLength);
                b.Property(x => x.CascadeId).HasMaxLength(BusinessConsts.MaxNotesLength);
                b.Property(x => x.Enabled).HasDefaultValue(false);

                b.HasIndex(q => q.Pid);
            });

            builder.Entity<Employee>(b =>
            {
                b.ToTable("base_employees");
                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();
                b.ConfigureSoftDelete();

                b.Property(x => x.Name).IsRequired().HasMaxLength(BusinessConsts.MaxNameLength);
                b.Property(x => x.Phone).IsRequired().HasMaxLength(BusinessConsts.MaxNameLength);
                b.Property(x => x.Email).IsRequired().HasMaxLength(BusinessConsts.MaxNotesLength);

                b.HasIndex(q => q.UserId);
            });

            builder.Entity<Job>(b =>
            {
                b.ToTable("base_jobs");
                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();
                b.ConfigureSoftDelete();

                b.Property(x => x.Name).IsRequired().HasMaxLength(BusinessConsts.MaxNameLength);
                b.Property(x => x.Description).HasMaxLength(BusinessConsts.MaxNotesLength);
            });

            builder.Entity<EmployeeJob>(b =>
            {
                b.ToTable("base_employee_jobs");
                b.HasKey(k => k.EmployeeId);
            });
        }
    }
}
