using Business.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Business.Test
{
    [Authorize(BusinessPermissions.DataDictionary.Default)]
    public class TestAppService : ApplicationService, ITestAppService
    {
        public async Task<string> TestApi(string name)
        {

            return "010101";
        }
    }
}
