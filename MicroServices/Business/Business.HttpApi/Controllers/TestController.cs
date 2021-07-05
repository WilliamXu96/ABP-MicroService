using Business.Test;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public async Task<string> GetAsync(string str)
        {
            return await _testAppService.TestApi(str);
        }

        [HttpGet]
        [Route("classes")]
        public List<string> GetClasses()
        {
            var theList = Assembly.GetExecutingAssembly().GetTypes()
                      .Where(t => t.Namespace == "Business.Models.Test")
                      .ToList();

            foreach (var the in theList)
            {
                var type = the.GetProperties();
            }

            return null;
        }

        [HttpGet]
        [Route("user-count")]
        public Task<long> GetUserCount()
        {
            return _testAppService.GetUserCount();
        }
    }
}
