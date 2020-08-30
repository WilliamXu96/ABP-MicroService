using BaseService.Systems.AuditLoggingManagement.Dto;
using BaseService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AuditLogging;

namespace BaseService.Systems.AuditLoggingManagement
{
    [Authorize(BaseServicePermissions.AuditLogging.Default)]
    public class AuditLoggingAppService: ApplicationService, IAuditLoggingAppService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public AuditLoggingAppService(
            IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AuditLogDto> Get(Guid id)
        {
            var auditLog = await _auditLogRepository.GetAsync(id);

            return ObjectMapper.Map<AuditLog, AuditLogDto>(auditLog);
        }

        public async Task<PagedResultDto<AuditLogDto>> GetAll(GetAuditLogsInput input)
        {
            var count = await _auditLogRepository.GetCountAsync(null, null, input.HttpMethod, input.Url,
                input.UserName, input.ApplicationName, input.CorrelationId, input.MaxExecutionDuration,
                input.MinExecutionDuration, input.HasException, input.HttpStatusCode);
            var list = await _auditLogRepository.GetListAsync(input.Sorting,
                input.MaxResultCount, input.SkipCount, null, null, input.HttpMethod, input.Url,
                input.UserName, input.ApplicationName, input.CorrelationId, input.MaxExecutionDuration,
                input.MinExecutionDuration, input.HasException, input.HttpStatusCode, true);

            return new PagedResultDto<AuditLogDto>(
                count,
                ObjectMapper.Map<List<AuditLog>, List<AuditLogDto>>(list)
            );
        }

        public async Task<GetAverageExecutionDurationPerDayOutput> GetAverageExecutionDurationPerDay(GetAverageExecutionDurationPerDayInput input)
        {
            var data = await _auditLogRepository.GetAverageExecutionDurationPerDayAsync(input.StartDate, input.EndDate);
            return new GetAverageExecutionDurationPerDayOutput()
            {

                Data = data
            };
        }
    }
}
