using BaseService.Systems.UserRoleMenusManagement.Dto;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.Systems.UserMenusManagement
{
    public interface IRoleMenusAppService : IApplicationService
    {
        Task<ListResultDto<RoleMenusDto>> GetRoleMenus();

        Task<ListResultDto<Guid>> GetRoleMenuIds(Guid id);

        Task<ListResultDto<MenusListDto>> GetMenusList();

        Task Update(UpdateRoleMenuDto input);
    }
}
