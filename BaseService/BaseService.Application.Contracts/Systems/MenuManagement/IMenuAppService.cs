using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseService.Systems.MenuManagement.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.Systems.MenuManagement
{
    public interface IMenuAppService : IApplicationService
    {
        Task<MenuDto> Create(CreateOrUpdateMenuDto input);

        Task Delete(List<Guid> ids);

        Task<MenuDto> Update(Guid id, CreateOrUpdateMenuDto input);

        Task<ListResultDto<MenuDto>> GetAll(GetMenuInputDto input);

        Task<MenuDto> Get(Guid id);

        Task<ListResultDto<MenuDto>> LoadAll(Guid? id);
    }
}
