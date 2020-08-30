using System;
using System.Collections.Generic;

namespace BaseService.Systems.AuditLoggingManagement.Dto
{
    public class GetAverageExecutionDurationPerDayOutput
    {
        public Dictionary<DateTime, double> Data { get; set; }
    }
}
