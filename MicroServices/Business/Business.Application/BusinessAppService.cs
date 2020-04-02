using Business.Localization;
using Volo.Abp.Application.Services;

namespace Business
{
    public abstract class BusinessAppService : ApplicationService
    {
        protected BusinessAppService()
        {
            LocalizationResource = typeof(BusinessResource);
        }
    }
}
