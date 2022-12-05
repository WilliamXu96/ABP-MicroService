using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace BaseService.BaseData
{
    public class DataDictionaryDetail : AuditedAggregateRoot<Guid>, ISoftDelete,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid Pid { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }

        public short Sort { get; set; }

        public bool IsDeleted { get; set; }

        public DataDictionaryDetail(Guid id, Guid pid, string label, string value, short sort)
        {
            Id = id;
            Pid = pid;
            Label = label;
            Value = value;
            Sort = sort;
        }
    }
}
