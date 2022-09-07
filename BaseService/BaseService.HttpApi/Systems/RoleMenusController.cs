using BaseService.Controllers;
using BaseService.Systems.UserMenusManagement;
using BaseService.Systems.UserMenusManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BaseService.HttpApi.Systems
{
    [Area("base")]
    [Route("api/base/role-menus")]
    public class RoleMenusController : BaseServiceController, IRoleMenusAppService
    {
        private readonly IRoleMenusAppService _roleMenusAppService;

        public RoleMenusController(IRoleMenusAppService roleMenusAppService)
        {
            _roleMenusAppService = roleMenusAppService;
        }

        [HttpGet]
        public Task<ListResultDto<RoleMenusDto>> GetRoleMenus()
        {
            return _roleMenusAppService.GetRoleMenus();
        }
    }
}
