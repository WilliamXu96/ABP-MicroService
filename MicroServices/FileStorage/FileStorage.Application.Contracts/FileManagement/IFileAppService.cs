using FileStorage.FileManagement.Dto;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FileStorage.FileManagement
{
    public interface IFileAppService : IApplicationService
    {
        Task<PagedResultDto<FileInfoDto>> GetAll(GetFileInputDto input);

        Task<FileInfoDto> Upload([Required] string name, [Required] IFormFile file);

        Task UploadPrivate([Required] string name, [Required] IFormFile file);
    }
}
