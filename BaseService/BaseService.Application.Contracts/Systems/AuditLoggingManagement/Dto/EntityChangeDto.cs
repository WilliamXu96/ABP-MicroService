using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace BaseService.Systems.AuditLoggingManagement.Dto
{
    public class EntityChangeDto : EntityDto<Guid>
    {
        public Guid AuditLogId { get; set; }

        public Guid? TenantId { get; set; }

        public DateTime ChangeTime { get; set; }

        public EntityChangeType ChangeType { get; set; }

        public Guid? EntityTenantId { get; set; }

        public string EntityId { get; set; }

        public string EntityTypeFullName { get; set; }

        public Collection<EntityPropertyChangeDto> PropertyChanges { get; set; }

        public Dictionary<string, object> ExtraProperties { get; set; }
    }
}
