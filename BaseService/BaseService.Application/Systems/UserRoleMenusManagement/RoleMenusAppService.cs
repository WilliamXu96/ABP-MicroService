using BaseService.Systems.UserRoleMenusManagement.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace BaseService.Systems.UserMenusManagement
{
    [Authorize]
    public class RoleMenusAppService : ApplicationService, IRoleMenusAppService
    {
        public IIdentityRoleRepository RoleRepository { get; }
        private readonly IRepository<Menu, Guid> _menuRepository;
        private readonly IRepository<RoleMenu> _roleMenuRepository;

        public RoleMenusAppService(
            IIdentityRoleRepository roleRepository,
            IRepository<Menu, Guid> menuRepository,
            IRepository<RoleMenu> roleMenuRepository)
        {
            RoleRepository = roleRepository;
            _menuRepository = menuRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        [Authorize(IdentityPermissions.Roles.ManagePermissions)]
        public async Task Update(UpdateRoleMenuDto input)
        {
            var roleMenus = new List<RoleMenu>();
            foreach (var menuId in input.MenuIds)
            {
                roleMenus.Add(new RoleMenu(input.RoleId, menuId));
            }
            await _roleMenuRepository.DeleteAsync(_ => _.RoleId == input.RoleId);
            await _roleMenuRepository.InsertManyAsync(roleMenus);
        }

        [Authorize(IdentityPermissions.Roles.Default)]
        public async Task<ListResultDto<MenusListDto>> GetMenusList()
        {
            var result = new List<Menu>();
            if (!CurrentTenant.Id.HasValue)
                result = await _menuRepository.GetListAsync();
            else
                result = await _menuRepository.GetListAsync(_ => _.IsHost == false);
            var dtos = ObjectMapper.Map<List<Menu>, List<MenusListDto>>(result);
            return new ListResultDto<MenusListDto>(dtos.OrderBy(_ => _.Sort).ToList());
        }

        /// <summary>
        /// 获取当前角色菜单
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<RoleMenusDto>> GetRoleMenus()
        {
            var roleIds = await (await RoleRepository.GetDbSetAsync()).Where(_ => CurrentUser.Roles.Contains(_.Name)).Select(_ => _.Id).ToListAsync();
            var roleMenus = await (await _roleMenuRepository.GetQueryableAsync()).Where(_ => roleIds.Contains(_.RoleId)).Select(_ => _.MenuId).ToListAsync();
            var menus = await _menuRepository.GetListAsync(_ => _.CategoryId == 1 && roleMenus.Contains(_.Id));
            var root = menus.Where(_ => _.Pid == null).OrderBy(_ => _.Sort).ToList();
            return new ListResultDto<RoleMenusDto>(LoadRoleMenusTree(root, menus));
        }

        [Authorize(IdentityPermissions.Roles.Default)]
        public async Task<ListResultDto<Guid>> GetRoleMenuIds(Guid id)
        {
            var menuIds = await _roleMenuRepository.GetListAsync(_ => _.RoleId == id);
            var menus = await _menuRepository.GetListAsync(_ => menuIds.Select(m => m.MenuId).Contains(_.Id));
            return new ListResultDto<Guid>(menus.Select(_ => _.Id).ToList());
        }

        private List<RoleMenusDto> LoadRoleMenusTree(List<Menu> roots, List<Menu> menus)
        {
            var result = new List<RoleMenusDto>();
            foreach (var root in roots)
            {
                var menu = new RoleMenusDto
                {
                    Path = root.Path,
                    Name = root.Name,
                    Component = root.Component,
                    Meta = new MenuMeta { Icon = root.Icon, Title = root.Title ?? root.Name, IsAffix = root.IsAffix },
                    AlwaysShow = root.AlwaysShow,
                    Hidden = root.Hidden
                };
                if (menus.Where(_ => _.Pid == root.Id).Any())
                {
                    menu.Children = LoadRoleMenusTree(menus.Where(_ => _.Pid == root.Id).OrderBy(_ => _.Sort).ToList(), menus);
                }
                result.Add(menu);
            }
            return result;
        }
    }
}
