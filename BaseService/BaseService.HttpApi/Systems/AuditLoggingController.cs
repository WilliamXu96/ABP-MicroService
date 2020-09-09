using BaseService.Controllers;
using BaseService.Systems.AuditLoggingManagement;
using BaseService.Systems.AuditLoggingManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BaseService.Systems
{
    [Area("base")]
    [Route("api/base/auditLogging")]
    public class AuditLoggingController : BaseServiceController, IAuditLoggingAppService
    {
        private readonly IAuditLoggingAppService _auditLoggingAppService;

        public AuditLoggingController(IAuditLoggingAppService auditLoggingAppService)
        {
            _auditLoggingAppService = auditLoggingAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<AuditLogDto> Get(Guid id)
        {
            return _auditLoggingAppService.Get(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<AuditLogDto>> GetAll(GetAuditLogsInput input)
        {
            return _auditLoggingAppService.GetAll(input);
        }

        [HttpGet]
        [Route("averageExecutionDurationPerDay")]
        public Task<GetAverageExecutionDurationPerDayOutput> GetAverageExecutionDurationPerDay(GetAverageExecutionDurationPerDayInput input)
        {
            return _auditLoggingAppService.GetAverageExecutionDurationPerDay(input);
        }
    }
}
