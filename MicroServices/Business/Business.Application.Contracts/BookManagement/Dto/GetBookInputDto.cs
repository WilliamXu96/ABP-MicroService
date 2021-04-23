using System;
using Volo.Abp.Application.Dtos;

namespace Business.BookManagement.Dto
{
    public class GetBookInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}