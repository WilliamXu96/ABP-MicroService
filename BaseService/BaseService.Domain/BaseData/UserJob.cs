using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace BaseService.BaseData
{
    public class UserJob : Entity, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid UserId { get; set; }

        public Guid JobId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { UserId, JobId };
        }

        public UserJob(Guid? tenantId, Guid userId, Guid jobId)
        {
            TenantId = tenantId;
            UserId = userId;
            JobId = jobId;
        }
    }
}
