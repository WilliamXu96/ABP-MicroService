using BaseService.Systems.TenantManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BaseService.Systems.TenantManagement
{
    public class TenantAppService : ApplicationService, ITenantAppService
    {
        public Task UpdateMenu(UpdateTenantMenuDto input)
        {
            throw new NotImplementedException();
        }
    }
}
