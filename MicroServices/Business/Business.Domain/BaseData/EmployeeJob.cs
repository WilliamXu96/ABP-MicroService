using System;
using Volo.Abp.Domain.Entities;

namespace Business.BaseData
{
    public class EmployeeJob : Entity
    {
        public Guid EmployeeId { get; set; }

        public Guid JobId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { EmployeeId, JobId };
        }

        public EmployeeJob(Guid employeeId, Guid jobId)
        {
            EmployeeId = employeeId;
            JobId = jobId;
        }
    }
}
