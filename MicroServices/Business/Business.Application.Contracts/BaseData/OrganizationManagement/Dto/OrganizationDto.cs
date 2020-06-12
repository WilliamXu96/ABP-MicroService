using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Business.BaseData.OrganizationManagement.Dto
{
    public class OrganizationDto : EntityDto<Guid>
    {
        public short CategoryId { get; set; }

        public Guid? Pid { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public int Sort { get; set; }

        public bool Enable { get; set; }
    }
}
