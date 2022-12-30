using FileStorage.Enums;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace FileStorage.Models
{
    public class FileManager : DomainService
    {
        private readonly IRepository<FileInfo, Guid> _repository;

        public FileManager(IRepository<FileInfo, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<FileInfo> Create(string name, string realName, string suffix, string md5, string size, string path, string url, FileType type)
        {
            return await _repository.InsertAsync(new FileInfo(GuidGenerator.Create(),
                                                                                name,
                                                                            realName,
                                                                              suffix,
                                                                                 md5,
                                                                                size,
                                                                                path,
                                                                                 url,
                                                                                type));
        }
    }
}
