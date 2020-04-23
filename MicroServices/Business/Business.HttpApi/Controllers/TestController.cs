using Business.Models.Test;
using Business.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Controllers
{
    [Route("api/test")]
    public class TestController: BusinessController
    {
        private readonly ITestAppService _testAppService;

        public TestController(ITestAppService testAppService)
        {
            _testAppService = testAppService;
        }

        [HttpGet]
        [Route("")]
        public async Task<string> GetAsync()
        {
            return await _testAppService.TestApi("1");
        }
    }
}
