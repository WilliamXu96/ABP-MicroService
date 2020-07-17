using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Business.BaseData.EmployeeManagement.Dto
{
    public class GetEmployeeInputDto : PagedAndSortedResultRequestDto
    {
        public Guid? OrgId { get; set; }

        public string Filter { get; set; }
    }
}
