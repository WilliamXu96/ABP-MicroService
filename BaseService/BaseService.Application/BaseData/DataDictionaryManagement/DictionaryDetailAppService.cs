using BaseService.BaseData.DataDictionaryManagement.Dto;
using BaseService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BaseService.BaseData.DataDictionaryManagement
{
    [Authorize(BaseServicePermissions.DataDictionary.Default)]
    public class DictionaryDetailAppService : ApplicationService, IDictionaryDetailAppService
    {
        private readonly IRepository<DataDictionary, Guid> _masterRepository;
        private readonly IRepository<DataDictionaryDetail, Guid> _detailRepository;

        public DictionaryDetailAppService(
            IRepository<DataDictionaryDetail, Guid> detailRepository,
            IRepository<DataDictionary, Guid> masterRepository)
        {
            _detailRepository = detailRepository;
            _masterRepository = masterRepository;
        }

        [Authorize(BaseServicePermissions.DataDictionary.Create)]
        public async Task<DictionaryDetailDto> Create(CreateOrUpdateDictionaryDetailDto input)
        {
            var master = await _masterRepository.FirstOrDefaultAsync(_ => _.Id == input.Pid);
            if (master == null)
            {
                throw new BusinessException("未找到字典！");
            }

            var exist = await _detailRepository.FirstOrDefaultAsync(_ => _.Label == input.Label);
            if (exist != null)
            {
                throw new BusinessException("名称：" + input.Label + "字典已存在");
            }

            var result = await _detailRepository.InsertAsync(new DataDictionaryDetail(
                                                                    GuidGenerator.Create(),
                                                                    CurrentTenant.Id,
                                                                    input.Pid,
                                                                    input.Label,
                                                                    input.Value,
                                                                    input.Sort));
            return ObjectMapper.Map<DataDictionaryDetail, DictionaryDetailDto>(result);
        }

        [Authorize(BaseServicePermissions.DataDictionary.Delete)]
        public async Task Delete(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await _detailRepository.DeleteAsync(id);
            }
        }

        public async Task<DictionaryDetailDto> Get(Guid id)
        {
            var result = await _detailRepository.GetAsync(id);
            return ObjectMapper.Map<DataDictionaryDetail, DictionaryDetailDto>(result);
        }

        public async Task<PagedResultDto<DictionaryDetailDto>> GetAll(GetDictionaryDetailInputDto input)
        {
            var query = _detailRepository.Where(_ => _.Pid == input.Pid);
            var items = await query.OrderBy(_ => _.Sort)
                     .Skip(input.SkipCount)
                     .Take(input.MaxResultCount)
                     .ToListAsync();

            var dots = ObjectMapper.Map<List<DataDictionaryDetail>, List<DictionaryDetailDto>>(items);
            var totalCount = await query.CountAsync();
            return new PagedResultDto<DictionaryDetailDto>(totalCount, dots);
        }

        [Authorize(BaseServicePermissions.DataDictionary.Update)]
        public async Task<DictionaryDetailDto> Update(Guid id, CreateOrUpdateDictionaryDetailDto input)
        {
            var detail = await _detailRepository.GetAsync(id);

            detail.Label = input.Label;
            detail.Value = input.Value;
            detail.Sort = input.Sort;

            return ObjectMapper.Map<DataDictionaryDetail, DictionaryDetailDto>(detail);
        }
    }
}
