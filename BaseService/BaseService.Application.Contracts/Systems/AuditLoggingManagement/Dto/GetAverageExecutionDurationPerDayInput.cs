using System;

namespace BaseService.Systems.AuditLoggingManagement.Dto
{
    public class GetAverageExecutionDurationPerDayInput
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
