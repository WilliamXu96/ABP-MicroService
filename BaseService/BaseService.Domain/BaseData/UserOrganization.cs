using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace BaseService.BaseData
{
    public class UserOrganization : Entity, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid UserId { get; set; }

        public Guid OrganizationId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { UserId, OrganizationId };
        }

        public UserOrganization(Guid? tenantId, Guid userId, Guid organizationId)
        {
            TenantId = tenantId;
            UserId = userId;
            OrganizationId = organizationId;
        }
    }
}
