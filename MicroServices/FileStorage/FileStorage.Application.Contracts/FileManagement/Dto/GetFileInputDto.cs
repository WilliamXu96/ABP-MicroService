using Volo.Abp.Application.Dtos;

namespace FileStorage.FileManagement.Dto
{
    public class GetFileInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
