using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace BaseService.Systems.AuditLoggingManagement.Dto
{
    public class AuditLogActionDto : EntityDto<Guid>
    {
        public Guid? TenantId { get; set; }

        public Guid AuditLogId { get; set; }

        public string ServiceName { get; set; }

        public string MethodName { get; set; }

        public string Parameters { get; set; }

        public DateTime ExecutionTime { get; set; }

        public int ExecutionDuration { get; set; }

        public Dictionary<string, object> ExtraProperties { get; set; }
    }
}
