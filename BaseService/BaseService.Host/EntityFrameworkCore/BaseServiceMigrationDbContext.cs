using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace BaseService.EntityFrameworkCore
{
    public class BaseServiceMigrationDbContext : AbpDbContext<BaseServiceMigrationDbContext>
    {
        public BaseServiceMigrationDbContext(
            DbContextOptions<BaseServiceMigrationDbContext> options
            ) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureTenantManagement();

            builder.ConfigureBaseService();
        }
    }
}
