using BaseService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BaseService.Controllers
{
    public abstract class BaseServiceController : AbpController
    {
        protected BaseServiceController()
        {
            LocalizationResource = typeof(BaseServiceResource);
        }
    }
}
