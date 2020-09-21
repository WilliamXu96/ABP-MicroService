using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace FileSystem.Models
{
    public class FileManager : DomainService
    {
        private readonly IRepository<FileInfo, Guid> _repository;

        public FileManager(IRepository<FileInfo, Guid> repository)
        {
            _repository = repository;
        }

        public async Task Create(string fileName, string md5, long size, string url)
        {
            await _repository.InsertAsync(new FileInfo(GuidGenerator.Create(),
                                                       CurrentTenant.Id,
                                                       fileName,
                                                       md5,
                                                       size,
                                                       url));
        }
    }
}
