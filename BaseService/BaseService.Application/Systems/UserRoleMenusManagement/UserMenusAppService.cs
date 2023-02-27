using BaseService.Systems.UserRoleMenusManagement.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace BaseService.Systems.UserRoleMenusManagement
{
    public class UserMenusAppService : ApplicationService, IUserMenusAppService
    {
        public IIdentityRoleRepository RoleRepository { get; }
        private readonly IRepository<Menu, Guid> _menuRepository;
        private readonly IRepository<RoleMenu> _roleMenuRepository;

        public UserMenusAppService(IIdentityRoleRepository roleRepository, IRepository<Menu, Guid> menuRepository, IRepository<RoleMenu> roleMenuRepository)
        {
            RoleRepository = roleRepository;
            _menuRepository = menuRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        [Authorize]
        public async Task<UserMenuPermissionsDto> GetUserMenuPermission()
        {
            var roleIds = await (await RoleRepository.GetDbSetAsync()).Where(_ => CurrentUser.Roles.Contains(_.Name)).Select(_ => _.Id).ToListAsync();
            var roleMenus = await (await _roleMenuRepository.GetQueryableAsync()).Where(_ => roleIds.Contains(_.RoleId)).Select(_ => _.MenuId).ToListAsync();
            var menus = await _menuRepository.GetListAsync(_ => _.CategoryId == 2 && roleMenus.Contains(_.Id));
            return new UserMenuPermissionsDto
            {
                Tenant = CurrentTenant.Name,
                UserName = CurrentUser.UserName,
                Name = CurrentUser.Name,
                MenuPermissions = menus.Select(_ => _.Permission).ToList()
            };
        }
    }
}
