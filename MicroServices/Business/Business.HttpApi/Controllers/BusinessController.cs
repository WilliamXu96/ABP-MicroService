using Business.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Business.Controllers
{
    public abstract class BusinessController : AbpController
    {
        protected BusinessController()
        {
            LocalizationResource = typeof(BusinessResource);
        }
    }
}
