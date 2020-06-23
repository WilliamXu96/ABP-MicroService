using Business.BaseData;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Business.EntityFrameworkCore
{
    [ConnectionStringName("Business")]
    public class BusinessDbContext : AbpDbContext<BusinessDbContext>
    {
        public DbSet<DataDictionary> DataDictionaries { get; set; }

        public DbSet<DataDictionaryDetail> DataDictionaryDetails { get; set; }

        public DbSet<Organization> organizations { get; set; }

        public DbSet<Employee> employees { get; set; }

        public DbSet<Job> jobs { get; set; }

        public DbSet<EmployeeJob> employeeJobs { get; set; }

        public BusinessDbContext(DbContextOptions<BusinessDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureBusiness();
        }
    }
}
