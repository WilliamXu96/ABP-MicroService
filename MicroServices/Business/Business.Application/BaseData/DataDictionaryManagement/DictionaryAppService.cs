using Business.BaseData.DataDictionaryManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Business.BaseData.DataDictionaryManagement
{
    public class DictionaryAppService : BusinessAppService, IDictionaryAppService
    {
        private readonly IRepository<DataDictionary, Guid> _repository;

        public DictionaryAppService(IRepository<DataDictionary, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<DictionaryDto> Create(CreateOrUpdateDictionaryDto input)
        {
            var exist = await _repository.GetAsync(_ => _.Name == input.Name);
            throw new NotImplementedException();
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
