using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseService.Controllers;
using BaseService.Systems.MenuManagement;
using BaseService.Systems.MenuManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace BaseService.HttpApi.Systems
{
    [Area("base")]
    [Route("api/base/menu")]
    public class MenuController : BaseServiceController, IMenuAppService
    {
        private readonly IMenuAppService _menuAppService;

        public MenuController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        [HttpPost]
        public Task<MenuDto> Create(CreateOrUpdateMenuDto input)
        {
            return _menuAppService.Create(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _menuAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<MenuDto> Get(Guid id)
        {
            return _menuAppService.Get(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<ListResultDto<MenuDto>> GetAll(GetMenuInputDto input)
        {
            return _menuAppService.GetAll(input);
        }

        [HttpGet]
        [Route("loadMenus")]
        public Task<ListResultDto<MenuDto>> LoadAll(Guid? id)
        {
            return _menuAppService.LoadAll(id);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<MenuDto> Update(Guid id, CreateOrUpdateMenuDto input)
        {
            return _menuAppService.Update(id, input);
        }
    }
}
