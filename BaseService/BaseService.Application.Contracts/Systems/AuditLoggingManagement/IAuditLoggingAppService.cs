using BaseService.Systems.AuditLoggingManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.Systems.AuditLoggingManagement
{
    public interface IAuditLoggingAppService : IApplicationService
    {
        Task<AuditLogDto> Get(Guid id);

        Task<PagedResultDto<AuditLogDto>> GetAll(GetAuditLogsInput input);

        Task<GetAverageExecutionDurationPerDayOutput> GetAverageExecutionDurationPerDay(GetAverageExecutionDurationPerDayInput input);
    }
}
