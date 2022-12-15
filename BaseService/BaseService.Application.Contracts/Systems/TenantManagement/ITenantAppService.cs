using BaseService.Systems.TenantManagement.Dto;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BaseService.Systems.TenantManagement
{
    public interface ITenantAppService : IApplicationService
    {
        Task UpdateMenu(UpdateTenantMenuDto input);
    }
}
