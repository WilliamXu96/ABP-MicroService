using Volo.Abp.Application.Dtos;

namespace BaseService.BaseData.DataDictionaryManagement.Dto
{
    public class GetDictionaryInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
