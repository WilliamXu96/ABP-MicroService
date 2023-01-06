using BaseService.Controllers;
using BaseService.Systems.UserMenusManagement;
using BaseService.Systems.UserRoleMenusManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace BaseService.HttpApi.Systems
{
    [RemoteService]
    [Area("base")]
    [Route("api/base/role-menus")]
    public class RoleMenusController : BaseServiceController, IRoleMenusAppService
    {
        private readonly IRoleMenusAppService _roleMenusAppService;

        public RoleMenusController(IRoleMenusAppService roleMenusAppService)
        {
            _roleMenusAppService = roleMenusAppService;
        }

        [HttpPost]
        [Route("update")]
        public Task Update(UpdateRoleMenuDto input)
        {
            return _roleMenusAppService.Update(input);
        }

        [HttpGet]
        [Route("list")]
        public Task<ListResultDto<MenusListDto>> GetMenusList()
        {
            return _roleMenusAppService.GetMenusList();
        }

        [HttpGet]
        public Task<ListResultDto<RoleMenusDto>> GetRoleMenus()
        {
            return _roleMenusAppService.GetRoleMenus();
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ListResultDto<Guid>> GetRoleMenuIds(Guid id)
        {
            return _roleMenusAppService.GetRoleMenuIds(id);
        }
    }
}
