using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Business.Test
{
    //[Authorize(BusinessPermissions.DataDictionary.Default)]
    public class TestAppService : ApplicationService, ITestAppService
    {
        private readonly IIdentityUserLookupAppService _identityUserLookupAppService;

        public TestAppService(IIdentityUserLookupAppService identityUserLookupAppService)
        {
            _identityUserLookupAppService = identityUserLookupAppService;
        }

        public async Task<string> TestApi(string name)
        {
            var arr = name.Split('.');

            return "010101";
        }

        public async Task<long> GetUserCount()
        {
            return await _identityUserLookupAppService.GetCountAsync(new UserLookupCountInputDto { Filter = null });
        }
    }
}
