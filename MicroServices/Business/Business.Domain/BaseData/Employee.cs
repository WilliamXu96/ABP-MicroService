using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Business.BaseData
{
    public class Employee : AuditedAggregateRoot<Guid>, ISoftDelete
    {
        public string Name { get; set; }

        /// <summary>
        /// 性别：0：女，1：男，2：其他
        /// </summary>
        public short Gender { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool Enabled { get; set; }

        public Guid OrgId { get; set; }

        public Guid? UserId { get; set; }

        public bool IsDeleted { get; set; }

        public Employee(Guid id, string name, short gender, string phone, string email, bool enabled, Guid orgId, Guid? userId)
        {
            Id = id;
            Name = name;
            Gender = gender;
            Phone = phone;
            Email = email;
            Enabled = enabled;
            OrgId = orgId;
            UserId = userId;
        }
    }
}
