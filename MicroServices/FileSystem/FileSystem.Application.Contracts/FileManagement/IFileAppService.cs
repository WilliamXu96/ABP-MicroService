using FileSystem.FileManagement.Dto;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FileSystem.FileManagement
{
    public interface IFileAppService : IApplicationService
    {
        Task<PagedResultDto<FileInfoDto>> GetAll(GetFileInputDto input);
    }
}
