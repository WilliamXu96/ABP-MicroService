using BaseService.Systems.UserMenusManagement.Dto;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.Systems.UserMenusManagement
{
    public interface IRoleMenusAppService : IApplicationService
    {
        Task<ListResultDto<RoleMenusDto>> GetRoleMenus();
    }
}
