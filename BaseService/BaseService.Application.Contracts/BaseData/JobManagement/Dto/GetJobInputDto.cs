using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BaseService.BaseData.JobManagement.Dto
{
    public class GetJobInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
