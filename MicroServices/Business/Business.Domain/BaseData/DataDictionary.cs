using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Business.BaseData
{
    public class DataDictionary: AuditedAggregateRoot<Guid>, ISoftDelete
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
