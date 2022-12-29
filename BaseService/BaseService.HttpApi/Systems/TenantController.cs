using BaseService.Controllers;
using BaseService.Systems.TenantManagement;
using BaseService.Systems.TenantManagement.Dto;
using BaseService.Systems.UserRoleMenusManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BaseService.HttpApi.Systems
{
    [Area("base")]
    [Route("api/base/tenant")]
    public class TenantController : BaseServiceController, ITenantAppService
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        [HttpGet]
        [Route("menu/{id}")]
        public Task<ListResultDto<Guid>> GetTenantMenuIds(Guid id)
        {
            return _tenantAppService.GetTenantMenuIds(id);
        }

        [HttpGet]
        [Route("menu-list")]
        public Task<ListResultDto<MenusTreeDto>> GetTenantMenusList()
        {
            return _tenantAppService.GetTenantMenusList();
        }

        [HttpPost]
        [Route("menu")]
        public Task UpdateTenantMenu(UpdateTenantMenuDto input)
        {
            return _tenantAppService.UpdateTenantMenu(input);
        }
    }
}
