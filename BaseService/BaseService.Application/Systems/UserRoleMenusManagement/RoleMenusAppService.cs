using BaseService.Systems.UserMenusManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BaseService.Systems.UserMenusManagement
{
    public class RoleMenusAppService : ApplicationService, IRoleMenusAppService
    {
        private readonly IRepository<Menu, Guid> _menuRepository;

        public RoleMenusAppService(IRepository<Menu, Guid> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<ListResultDto<RoleMenusDto>> GetRoleMenus()
        {
            var menus = await _menuRepository.GetListAsync();
            var root = menus.Where(_ => _.Pid == null).OrderBy(_ => _.Sort).ToList();
            return new ListResultDto<RoleMenusDto>(LoadTree(root, menus));
        }

        private List<RoleMenusDto> LoadTree(List<Menu> roots, List<Menu> menus)
        {
            var result = new List<RoleMenusDto>();
            foreach (var root in roots)
            {
                var menu = new RoleMenusDto
                {
                    Path = root.Path,
                    Name = root.Name,
                    Component = root.Component,
                    Meta = new MenuMeta { Icon = root.Icon, Title = root.Name },
                    AlwaysShow = root.AlwaysShow,
                    Hidden = root.Hidden
                };
                if (menus.Where(_ => _.Pid == root.Id).Any())
                {
                    menu.Children = LoadTree(menus.Where(_ => _.Pid == root.Id).OrderBy(_ => _.Sort).ToList(), menus);
                }
                result.Add(menu);
            }
            return result;
        }
    }
}
