using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.BaseData.OrganizationManagement.Dto
{
    public class CreateOrUpdateOrganizationDto
    {
        public short CategoryId { get; set; }

        public Guid? Pid { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
