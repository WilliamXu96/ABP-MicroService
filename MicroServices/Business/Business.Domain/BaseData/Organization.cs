using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Business.BaseData
{
    public class Organization : AuditedAggregateRoot<Guid>, ISoftDelete
    {
        /// <summary>
        /// 机构分类：1.公司；2.组织；3.部门；4.供应商
        /// </summary>
        public short CategoryId { get; set; }

        public Guid? Pid { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public int Sort { get; set; }

        public bool Enabled { get; set; }

        public bool HasChildren { get; set; }

        public bool Leaf { get; set; }

        public bool IsDeleted { get; set; }

        public Organization(Guid id, short categoryId, Guid? pid, [NotNull]string name, string fullName, int sort, bool enabled, bool hasChildren = false, bool leaf = false)
        {
            Id = id;
            CategoryId = categoryId;
            Pid = pid;
            Name = name;
            FullName = fullName;
            Sort = sort;
            Enabled = enabled;
            HasChildren = hasChildren;
        }
    }
}
