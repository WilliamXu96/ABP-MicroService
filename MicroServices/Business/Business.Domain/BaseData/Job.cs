using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Business.BaseData
{
    /// <summary>
    /// 岗位
    /// </summary>
    public class Job : AuditedAggregateRoot<Guid>, ISoftDelete
    {
        public string Name { get; set; }

        public bool Enabled { get; set; }

        public int Sort { get; set; }

        public bool IsDeleted { get; set; }

        public Job(Guid id, string name, bool enabled, int sort)
        {
            Id = id;
            Name = name;
            Enabled = enabled;
            Sort = sort;
        }
    }
}
