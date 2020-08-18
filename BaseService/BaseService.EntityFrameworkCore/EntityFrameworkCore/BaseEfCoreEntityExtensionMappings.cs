using Volo.Abp.Threading;

namespace BaseService.EntityFrameworkCore
{
    public class BaseEfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            BaseServiceModuleExtensionConfigurator.Configure();

            //OneTimeRunner.Run(() =>
            //{
            //    ObjectExtensionManager.Instance
            //        .MapEfCoreProperty<IdentityUser, bool>(
            //            "Enable",
            //            b => { b.HasDefaultValue(false); }
            //        );
            //});
        }
    }
}
