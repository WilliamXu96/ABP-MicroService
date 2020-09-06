using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace BaseService.BaseData
{
    public class DataDictionary : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DataDictionary(Guid id, Guid? tenantId, [NotNull] string name, string description)
        {
            TenantId = tenantId;
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
