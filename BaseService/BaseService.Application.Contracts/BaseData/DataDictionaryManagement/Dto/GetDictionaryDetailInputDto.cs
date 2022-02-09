using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace BaseService.BaseData.DataDictionaryManagement.Dto
{
    public class GetDictionaryDetailInputDto: PagedAndSortedResultRequestDto
    {
        [Required]
        public Guid Pid { get; set; }
    }
}
