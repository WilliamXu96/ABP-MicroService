using Business.BaseData.DataDictionaryManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Business.BaseData.DataDictionaryManagement
{
    public class DictionaryAppService : ApplicationService, IDictionaryAppService
    {
        private readonly IRepository<DataDictionary, Guid> _repository;

        public DictionaryAppService(IRepository<DataDictionary, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<DictionaryDto> Create(CreateOrUpdateDictionaryDto input)
        {
            var exist = _repository.FirstOrDefault(_ => _.Name == input.Name);

            if (exist != null)
            {
                throw new BusinessException("名称：" + input.Name + "字典已存在");
            }

            var dic = new DataDictionary(
                GuidGenerator.Create(),
                input.Name,
                input.Description);

            var result = await _repository.InsertAsync(dic);
            return ObjectMapper.Map<DataDictionary, DictionaryDto>(result);
        }

        public Task Delete(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<DictionaryDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<DictionaryDto>> GetAll(GetDictionaryInputDto input)
        {
            throw new NotImplementedException();
        }

        public Task<DictionaryDto> Update(Guid id, CreateOrUpdateDictionaryDto input)
        {
            throw new NotImplementedException();
        }
    }
}
