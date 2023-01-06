using BaseService.Systems.TenantManagement.Dto;
using BaseService.Systems.UserRoleMenusManagement.Dto;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.Systems.TenantManagement
{
    public interface ITenantAppService : IApplicationService
    {
        Task UpdateTenantMenu(UpdateTenantMenuDto input);

        Task<ListResultDto<MenusListDto>> GetTenantMenusList();

        Task<ListResultDto<MenusListDto>> GetTenantMenusById(Guid id);
    }
}
