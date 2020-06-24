using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Business.BaseData.JobManagement.Dto
{
    public class GetJobInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
