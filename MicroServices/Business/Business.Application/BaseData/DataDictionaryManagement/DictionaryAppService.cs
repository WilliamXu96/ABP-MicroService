using Business.BaseData.DataDictionaryManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Business.BaseData.DataDictionaryManagement
{
    public class DictionaryAppService : BusinessAppService, IDictionaryAppService
    {
        public Task<DictionaryDto> Create(CreateOrUpdateDictionaryDto input)
        {
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
