using FileStorage.FileManagement.Dto;
using FileStorage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FileStorage.FileManagement
{
    public class FileAppService : ApplicationService, IFileAppService
    {
        private readonly IRepository<FileInfo, Guid> _repository;

        public FileAppService(IRepository<FileInfo, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<FileInfoDto>> GetAll(GetFileInputDto input)
        {
            var query = _repository.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Name")
                                   .Skip(input.SkipCount)
                                   .Take(input.MaxResultCount)
                                   .ToListAsync();

            var dtos = ObjectMapper.Map<List<FileInfo>, List<FileInfoDto>>(items);
            return new PagedResultDto<FileInfoDto>(totalCount, dtos);
        }
    }
}
