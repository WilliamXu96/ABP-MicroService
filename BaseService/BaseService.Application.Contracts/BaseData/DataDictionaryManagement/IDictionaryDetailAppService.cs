using BaseService.BaseData.DataDictionaryManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.BaseData.DataDictionaryManagement
{
    public interface IDictionaryDetailAppService : IApplicationService
    {
        Task<PagedResultDto<DictionaryDetailDto>> GetAll(GetDictionaryDetailInputDto input);

        Task<DictionaryDetailDto> Get(Guid id);

        Task<DictionaryDetailDto> Create(CreateOrUpdateDictionaryDetailDto input);

        Task<DictionaryDetailDto> Update(Guid id, CreateOrUpdateDictionaryDetailDto input);

        Task Delete(List<Guid> ids);
    }
}
