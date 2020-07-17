using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BaseData.EmployeeManagement.Dto
{
    public class CreateOrUpdateEmployeeDto
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
    }
}
