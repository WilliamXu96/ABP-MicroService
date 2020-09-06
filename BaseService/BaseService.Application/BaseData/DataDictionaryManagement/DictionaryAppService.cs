using BaseService.BaseData.DataDictionaryManagement.Dto;
using BaseService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace BaseService.BaseData.DataDictionaryManagement
{
    [Authorize(BaseServicePermissions.DataDictionary.Default)]
    public class DictionaryAppService : ApplicationService, IDictionaryAppService
    {
        private readonly IRepository<DataDictionary, Guid> _repository;

        public DictionaryAppService(IRepository<DataDictionary, Guid> repository)
        {
            _repository = repository;
        }

        [Authorize(BaseServicePermissions.DataDictionary.Create)]
        public async Task<DictionaryDto> Create(CreateOrUpdateDictionaryDto input)
        {
            var exist = await _repository.FirstOrDefaultAsync(_ => _.Name == input.Name);

            if (exist != null)
            {
                throw new BusinessException("名称：" + input.Name + "字典已存在");
            }

            var dic = new DataDictionary(
                GuidGenerator.Create(),
                CurrentTenant.Id,
                input.Name,
                input.Description);

            var result = await _repository.InsertAsync(dic);
            return ObjectMapper.Map<DataDictionary, DictionaryDto>(result);
        }

        [Authorize(BaseServicePermissions.DataDictionary.Delete)]
        public async Task Delete(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await _repository.DeleteAsync(id);
            }
        }

        public async Task<DictionaryDto> Get(Guid id)
        {
            var result = await _repository.GetAsync(id);
            return ObjectMapper.Map<DataDictionary, DictionaryDto>(result);
        }

        public async Task<PagedResultDto<DictionaryDto>> GetAll(GetDictionaryInputDto input)
        {
            var query = _repository
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.Name.Contains(input.Filter) ||
                                                                        _.Description.Contains(input.Filter));

            var items = await query.OrderBy(input.Sorting ?? "Name")
                                 .Skip(input.SkipCount)
                                 .Take(input.MaxResultCount)
                                 .ToListAsync();
            var totalCount = await query.CountAsync();

            var dots = ObjectMapper.Map<List<DataDictionary>, List<DictionaryDto>>(items);
            return new PagedResultDto<DictionaryDto>(totalCount, dots);
        }

        [Authorize(BaseServicePermissions.DataDictionary.Update)]
        public async Task<DictionaryDto> Update(Guid id, CreateOrUpdateDictionaryDto input)
        {
            var dic = await _repository.GetAsync(id);

            dic.Name = input.Name;
            dic.Description = input.Description;

            return ObjectMapper.Map<DataDictionary, DictionaryDto>(dic);
        }
    }
}
