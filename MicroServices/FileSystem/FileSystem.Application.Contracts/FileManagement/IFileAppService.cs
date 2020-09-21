using FileSystem.FileManagement.Dto;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace FileSystem.FileManagement
{
    public interface IFileAppService : IApplicationService
    {
        Task Create(CreateOrUpdateFileDto input);
    }
}
