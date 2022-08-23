using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using BaseService.Permissions;
using BaseService.Systems.MenuManagement.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BaseService.Systems.MenuManagement
{
    [Authorize(BaseServicePermissions.Menu.Default)]
    public class MenuAppService : ApplicationService, IMenuAppService
    {
        private readonly IRepository<Menu, Guid> _repository;

        public MenuAppService(IRepository<Menu, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<MenuDto> Create(CreateOrUpdateMenuDto input)
        {
            var exist = await _repository.FirstOrDefaultAsync(_ => _.Name == input.Name);
            if (exist != null) throw new BusinessException("名称：" + input.Name + "菜单已存在");

            var result = await _repository.InsertAsync(new Menu(GuidGenerator.Create())
            {
                FormId = input.FormId,
                Pid = input.Pid,
                Name = input.Name,
                CategoryId = input.CategoryId,
                Sort = input.Sort,
                Route = input.Route,
                Permission = input.Permission,
                Icon = input.Icon
            });
            return ObjectMapper.Map<Menu, MenuDto>(result);
        }

        public async Task Delete(List<Guid> ids)
        {
            //TODO：删除子集
            foreach (var id in ids)
            {
                await _repository.DeleteAsync(id);
            }
        }

        public async Task<PagedResultDto<MenuDto>> GetAll(GetMenuInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Sort")
                                   .Skip(input.SkipCount)
                                   .Take(input.MaxResultCount)
                                   .ToListAsync();

            var dtos = ObjectMapper.Map<List<Menu>, List<MenuDto>>(items);
            return new PagedResultDto<MenuDto>(totalCount, dtos);
        }

        public async Task<MenuDto> Get(Guid id)
        {
            var result = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<Menu, MenuDto>(result);
            if (dto.Pid.HasValue)
                dto.ParentName = (await _repository.FirstOrDefaultAsync(_ => _.Id == result.Pid))?.Name;
            return dto;
        }

        public async Task<ListResultDto<MenuDto>> LoadAll(Guid? id)
        {
            var items = await (await _repository.GetQueryableAsync()).Where(_ => _.Pid == id).OrderBy(_ => _.Sort).ToListAsync();
            var dtos = ObjectMapper.Map<List<Menu>, List<MenuDto>>(items);
            return new ListResultDto<MenuDto>(dtos);
        }

        public async Task<MenuDto> Update(Guid id, CreateOrUpdateMenuDto input)
        {
            var menu = await _repository.GetAsync(id);
            menu.Pid = input.Pid;
            menu.CategoryId = input.CategoryId;
            //TODO：菜单名重复验证
            menu.Name = input.Name;
            menu.Sort = input.Sort;
            menu.Route = input.Route;
            menu.Icon = input.Icon;
            menu.Permission = input.Permission;

            return ObjectMapper.Map<Menu, MenuDto>(menu);
        }
    }
}
