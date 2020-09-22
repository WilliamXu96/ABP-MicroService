using Volo.Abp.Application.Dtos;

namespace FileSystem.FileManagement.Dto
{
    public class GetFileInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
