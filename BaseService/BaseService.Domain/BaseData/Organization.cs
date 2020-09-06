using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace BaseService.BaseData
{
    public class Organization : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 机构分类：1.公司；2.组织；3.部门；4.供应商
        /// </summary>
        public short CategoryId { get; set; }

        public Guid? Pid { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public int Sort { get; set; }

        /// <summary>
        /// 是否子集
        /// </summary>
        public bool Leaf { get; set; }

        /// <summary>
        /// 级联
        /// </summary>
        public string CascadeId { get; set; }

        public bool Enabled { get; set; }

        public bool IsDeleted { get; set; }

        public Organization(Guid id, Guid? tenantId, short categoryId, Guid? pid, [NotNull]string name, string fullName, int sort, bool leaf, bool enabled)
        {
            TenantId = tenantId;
            Id = id;
            CategoryId = categoryId;
            Pid = pid;
            Name = name;
            FullName = fullName;
            Sort = sort;
            Enabled = enabled;
            Leaf = leaf;
        }
    }
}
