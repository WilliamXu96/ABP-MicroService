using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;

namespace AuthServer.Host.EntityFrameworkCore
{
    public class AuthServerDbContext : AbpDbContext<AuthServerDbContext>
    {
        public AuthServerDbContext(DbContextOptions<AuthServerDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureIdentityServer();
        }
    }
}
