using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace BaseService.BaseData
{
    /// <summary>
    /// 岗位
    /// </summary>
    public class Job : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public string Name { get; set; }

        public bool Enabled { get; set; }

        public int Sort { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public Job(Guid id, Guid? tenantId, string name, bool enabled, int sort, string description)
        {
            TenantId = tenantId;
            Id = id;
            Name = name;
            Enabled = enabled;
            Sort = sort;
            Description = description;
        }
    }
}
