using Business.BaseData.DataDictionaryManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Business.BaseData.DataDictionaryManagement
{
    public class DictionaryDetailAppService : BusinessAppService, IDictionaryDetailAppService
    {
        public Task<DictionaryDetailDto> Create(CreateOrUpdateDictionaryDetailDto input)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<DictionaryDetailDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<DictionaryDetailDto>> GetAll(Guid pid)
        {
            throw new NotImplementedException();
        }

        public Task<DictionaryDetailDto> Update(Guid id, CreateOrUpdateDictionaryDetailDto input)
        {
            throw new NotImplementedException();
        }
    }
}
