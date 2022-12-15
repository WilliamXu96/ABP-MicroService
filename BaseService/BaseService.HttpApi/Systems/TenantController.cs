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

        [HttpPost]
        [Route("update")]
        public Task UpdateMenu(UpdateTenantMenuDto input)
        {
            return null;
        }
    }
}
