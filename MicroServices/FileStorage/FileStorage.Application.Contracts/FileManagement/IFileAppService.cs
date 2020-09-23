using FileStorage.FileManagement.Dto;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FileStorage.FileManagement
{
    public interface IFileAppService : IApplicationService
    {
        Task<PagedResultDto<FileInfoDto>> GetAll(GetFileInputDto input);
    }
}
