using BaseService.Systems.TenantManagement.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;

namespace BaseService.Systems.TenantManagement
{
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

        public async Task CreateTenantMenu(UpdateTenantMenuDto input)
        {
            var tenant = await TenantRepository.GetAsync(input.TenantId);
            var menus = await (await _menuRepository.GetQueryableAsync()).Where(_ => input.MenuIds.Contains(_.Id)).ToListAsync();
            using (CurrentTenant.Change(tenant.Id))
            {
                var tenantRoles = await RoleRepository.GetListAsync();
                //清除租户所有角色菜单，TODO：清除租户角色权限
                await _roleMenuRepository.DeleteAsync(_ => tenantRoles.Select(r => r.Id).Contains(_.RoleId));

                foreach (var menu in menus)
                {
                    //添加租户菜单
                    var menuId = GuidGenerator.Create();
                    await _menuRepository.InsertAsync(new Menu(menuId)
                    {
                        FormId = menu.FormId,
                        Pid = menu.Pid,
                        CategoryId = menu.CategoryId,
                        Name = menu.Name,
                        Label = menu.Label,
                        Sort = menu.Sort,
                        Path = menu.Path,
                        Component = menu.Component,
                        Permission = menu.Permission,
                        Icon = menu.Icon,
                        Hidden = menu.Hidden,
                        AlwaysShow = menu.AlwaysShow
                    });
                    //添加租户admin角色菜单
                    await _roleMenuRepository.InsertAsync(new RoleMenu(tenantRoles.FirstOrDefault(t => t.Name == "admin").Id, menuId));
                }
            }
        }
    }
}
