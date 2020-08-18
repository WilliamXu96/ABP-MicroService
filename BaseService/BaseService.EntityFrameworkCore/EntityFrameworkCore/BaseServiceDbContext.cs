using BaseService.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace BaseService.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class BaseServiceDbContext : AbpDbContext<BaseServiceDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

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
