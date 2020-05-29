using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Business.BaseData.DataDictionaryManagement.Dto
{
    public class GetDictionaryInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
