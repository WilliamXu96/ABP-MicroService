using BaseService.Controllers;
using BaseService.Systems.TenantManagement;
using BaseService.Systems.TenantManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("menu")]
        public Task CreateTenantMenu(UpdateTenantMenuDto input)
        {
            return _tenantAppService.CreateTenantMenu(input);
        }
    }
}
