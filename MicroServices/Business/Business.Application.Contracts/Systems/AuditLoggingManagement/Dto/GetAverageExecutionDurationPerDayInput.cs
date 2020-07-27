using System;

namespace Business.Systems.AuditLoggingManagement.Dto
{
    public class GetAverageExecutionDurationPerDayInput
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
