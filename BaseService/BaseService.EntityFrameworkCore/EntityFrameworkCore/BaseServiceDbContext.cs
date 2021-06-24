using BaseService.BaseData;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace BaseService.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class BaseServiceDbContext : AbpDbContext<BaseServiceDbContext>
    {
        public DbSet<DataDictionary> DataDictionaries { get; set; }

        public DbSet<DataDictionaryDetail> DataDictionaryDetails { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<UserJob> UserJobs { get; set; }

        public DbSet<UserOrganization> UserOrganizations { get; set; }

        public BaseServiceDbContext(DbContextOptions<BaseServiceDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //builder.Entity<AppUser>(b =>
            //{
            //    b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

            //    b.ConfigureByConvention();
            //    b.ConfigureAbpUser();

            //    b.Property(x => x.Enable).HasDefaultValue(true);

            //});

            builder.ConfigureBaseService();
        }
    }
}
