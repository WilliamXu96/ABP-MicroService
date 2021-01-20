using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Business.Test
{
    public interface ITestAppService: IApplicationService
    {
        Task<string> TestApi(string name);

        Task<long> GetUserCount();
    }
}
