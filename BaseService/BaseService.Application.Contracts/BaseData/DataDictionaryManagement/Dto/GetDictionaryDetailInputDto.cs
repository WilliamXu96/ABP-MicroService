using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BaseService.BaseData.DataDictionaryManagement.Dto
{
    public class GetDictionaryDetailInputDto: PagedAndSortedResultRequestDto
    {
        [Required]
        public Guid Pid { get; set; }
    }
}
