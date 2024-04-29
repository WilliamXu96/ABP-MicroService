//using BaseService.Systems.UserMenusManagement;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Volo.Abp.Application.Services;
//using Volo.Abp.Identity;

//namespace Business.Test
//{
//    /// <summary>
//    /// 内部网关接口示例
//    /// </summary>
//    public class TestAppService : ApplicationService, ITestAppService
//    {
//        private readonly IIdentityUserLookupAppService _identityUserLookupAppService;
//        private readonly IRoleMenusAppService _roleMenusAppService;

//        public TestAppService(
//            IIdentityUserLookupAppService identityUserLookupAppService,
//            IRoleMenusAppService roleMenusAppService
//            )
//        {
//            _identityUserLookupAppService = identityUserLookupAppService;
//            _roleMenusAppService = roleMenusAppService;
//        }

//        public async Task<string> TestApi(string name)
//        {
//            var arr = name.Split('.');

//            return "010101";
//        }

//        /// <summary>
//        /// 内部网关-用户数量
//        /// </summary>
//        /// <returns></returns>
//        public async Task<long> GetUserCount()
//        {
//            return await _identityUserLookupAppService.GetCountAsync(new UserLookupCountInputDto { Filter = null });
//        }

//        /// <summary>
//        /// 内部网关-菜单树形
//        /// </summary>
//        /// <returns></returns>
//        public async Task<dynamic> GetMenuTree()
//        {
//            return await _roleMenusAppService.GetMenusList();
//        }
//    }
//}
