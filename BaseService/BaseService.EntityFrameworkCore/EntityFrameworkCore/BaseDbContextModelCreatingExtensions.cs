using BaseService.BaseData;
using BaseService.Systems;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace BaseService.EntityFrameworkCore
{
    public static class BaseDbContextModelCreatingExtensions
    {
        public static void ConfigureBaseService(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<DataDictionary>(b =>
            {
                b.ToTable("base_dict");

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(BaseServiceConsts.MaxNameLength);
                b.Property(x => x.Description).HasMaxLength(BaseServiceConsts.MaxNotesLength);
                b.Property(x => x.IsDeleted).HasDefaultValue(false);

                b.HasIndex(q => q.Name);
            });

            builder.Entity<DataDictionaryDetail>(b =>
            {
                b.ToTable("base_dict_details");

                b.ConfigureByConvention();

                b.Property(x => x.Label).IsRequired().HasMaxLength(BaseServiceConsts.MaxNameLength);
                b.Property(x => x.Value).IsRequired().HasMaxLength(BaseServiceConsts.MaxNotesLength);
                b.Property(x => x.IsDeleted).HasDefaultValue(false);

                b.HasIndex(q => q.Pid);
            });

            builder.Entity<Organization>(b =>
            {
                b.ToTable("base_orgs");

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(BaseServiceConsts.MaxNameLength);
                b.Property(x => x.FullName).IsRequired().HasMaxLength(BaseServiceConsts.MaxFullNameLength);
                b.Property(x => x.CascadeId).HasMaxLength(BaseServiceConsts.MaxNotesLength);
                b.Property(x => x.Enabled).HasDefaultValue(false);

                b.HasIndex(q => q.Pid);
            });

            builder.Entity<Job>(b =>
            {
                b.ToTable("base_jobs");

                b.ConfigureByConvention();


                b.Property(x => x.Name).IsRequired().HasMaxLength(BaseServiceConsts.MaxNameLength);
                b.Property(x => x.Description).HasMaxLength(BaseServiceConsts.MaxNotesLength);
            });

            builder.Entity<UserJob>(b =>
            {
                b.ToTable("base_user_jobs");

                b.HasKey(k => new { k.UserId, k.JobId });
            });

            builder.Entity<UserOrganization>(b =>
            {
                b.ToTable("base_user_orgs");

                b.HasKey(k => new { k.UserId, k.OrganizationId });
            });

            builder.Entity<Menu>(b =>
            {
                b.ToTable("base_menu");

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(BaseServiceConsts.MaxNameLength);
                b.Property(x => x.Path).HasMaxLength(BaseServiceConsts.MaxNotesLength);
                b.Property(x => x.Component).HasMaxLength(BaseServiceConsts.MaxNotesLength);
                b.Property(x => x.Permission).HasMaxLength(BaseServiceConsts.MaxNotesLength);
                b.Property(x => x.Icon).HasMaxLength(BaseServiceConsts.MaxFullNameLength);
                b.Property(x => x.Label).IsRequired().HasMaxLength(BaseServiceConsts.MaxFullNameLength);
                b.Property(x => x.IsHost).HasDefaultValue(false);
            });

            builder.Entity<RoleMenu>(b =>
            {
                b.ToTable("base_role_menu");

                b.HasKey(k => new { k.RoleId, k.MenuId });
            });
        }
    }
}
