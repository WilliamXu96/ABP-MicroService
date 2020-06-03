using Business.Test;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Business.Controllers
{
    [Area("business")]
    [Route("api/business/test")]
    public class TestController: BusinessController
    {
        private readonly ITestAppService _testAppService;

        public TestController(ITestAppService testAppService)
        {
            _testAppService = testAppService;
        }

        [HttpGet]
        public async Task<string> GetAsync()
        {
            return await _testAppService.TestApi("1");
        }
    }
}
