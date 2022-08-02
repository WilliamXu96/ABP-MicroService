using Volo.Abp.Application.Dtos;

namespace BaseService.Systems.MenuManagement.Dto
{
    public class GetMenuInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
