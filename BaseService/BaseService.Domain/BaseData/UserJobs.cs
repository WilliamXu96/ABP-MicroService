using System;
using Volo.Abp.Domain.Entities;

namespace BaseService.BaseData
{
    public class UserJobs : Entity
    {
        public Guid UserId { get; set; }

        public Guid JobId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { UserId, JobId };
        }

        public UserJobs(Guid userId, Guid jobId)
        {
            UserId = userId;
            JobId = jobId;
        }
    }
}
