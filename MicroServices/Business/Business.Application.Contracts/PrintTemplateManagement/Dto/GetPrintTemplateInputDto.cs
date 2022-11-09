using System;
using Volo.Abp.Application.Dtos;

namespace Business.PrintTemplateManagement.Dto
{
    public class GetPrintTemplateInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}