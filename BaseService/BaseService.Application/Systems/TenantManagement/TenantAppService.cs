using BaseService.Systems.TenantManagement.Dto;
using BaseService.Systems.UserRoleMenusManagement.Dto;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;

namespace BaseService.Systems.TenantManagement
{
    [Authorize(TenantManagementPermissions.Tenants.Default)]
    public class TenantAppService : ApplicationService, ITenantAppService
    {
        protected ITenantRepository TenantRepository { get; }
        public IIdentityRoleRepository RoleRepository { get; }
        private readonly IRepository<Menu, Guid> _menuRepository;
        private readonly IRepository<RoleMenu> _roleMenuRepository;

        public TenantAppService(
            IRepository<Menu, Guid> menuRepository,
            ITenantRepository tenantRepository,
            IIdentityRoleRepository roleRepository,
            IRepository<RoleMenu> roleMenuRepository)
        {
            _menuRepository = menuRepository;
            TenantRepository = tenantRepository;
            RoleRepository = roleRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        public async Task<ListResultDto<MenusListDto>> GetTenantMenusList()
        {
            var result = await _menuRepository.GetListAsync(_ => _.IsHost == false);
            var dtos = ObjectMapper.Map<List<Menu>, List<MenusListDto>>(result);
            return new ListResultDto<MenusListDto>(dtos.OrderBy(_ => _.Sort).ToList());
        }

        public async Task<ListResultDto<MenusListDto>> GetTenantMenusById(Guid id)
        {
            var tenant = await TenantRepository.GetAsync(id);
            using (CurrentTenant.Change(tenant.Id))
            {
                var result = await _menuRepository.GetListAsync();
                var dtos = ObjectMapper.Map<List<Menu>, List<MenusListDto>>(result);
                return new ListResultDto<MenusListDto>(dtos.OrderBy(_ => _.Sort).ToList());
            }
        }

        public async Task UpdateTenantMenu(UpdateTenantMenuDto input)
        {
            var tenant = await TenantRepository.GetAsync(input.TenantId);
            using (CurrentTenant.Change(tenant.Id))
            {

                var menus = await _menuRepository.GetListAsync();
                var adminRole = (await RoleRepository.GetListAsync()).First(t => t.Name == "admin");

                //清除租户所有角色菜单，TODO：清除租户角色权限
                await _roleMenuRepository.DeleteAsync(_ => menus.Select(m => m.Id).Contains(_.MenuId));

                foreach (var menu in menus)
                {
                    if (input.MenuIds.Contains(menu.Id))
                    {
                        menu.IsHost = false;
                        //添加租户admin角色菜单
                        await _roleMenuRepository.InsertAsync(new RoleMenu(adminRole.Id, menu.Id));
                    }
                    else
                        menu.IsHost = true;
                    
                }
            }
        }
    }
}
