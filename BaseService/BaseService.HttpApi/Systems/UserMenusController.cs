using BaseService.Controllers;
using BaseService.Systems.UserRoleMenusManagement;
using BaseService.Systems.UserRoleMenusManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;

namespace BaseService.HttpApi.Systems
{
    [RemoteService]
    [Area("base")]
    [Route("api/base/user-menus")]
    public class UserMenusController : BaseServiceController, IUserMenusAppService
    {
        private readonly IUserMenusAppService _userMenusAppService;

        public UserMenusController(IUserMenusAppService userMenusAppService)
        {
            _userMenusAppService = userMenusAppService;
        }

        [HttpGet]
        [Route("permissions")]
        public Task<UserMenuPermissionsDto> GetUserMenuPermission()
        {
            return _userMenusAppService.GetUserMenuPermission();
        }
    }
}
