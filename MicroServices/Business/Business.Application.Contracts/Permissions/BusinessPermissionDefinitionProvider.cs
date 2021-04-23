using Business.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Business.Permissions
{
    public class BusinessPermissionDefinitionProvider: PermissionDefinitionProvider
    {

        public override void Define(IPermissionDefinitionContext context)
        {
            var business = context.AddGroup(BusinessPermissions.Business, L("Business"), MultiTenancySides.Tenant);

            var Book = business.AddPermission(BusinessPermissions.Book.Default, L("Book"));
            Book.AddChild(BusinessPermissions.Book.Update, L("Edit"));
            Book.AddChild(BusinessPermissions.Book.Delete, L("Delete"));
            Book.AddChild(BusinessPermissions.Book.Create, L("Create"));

            //Code generation...
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BusinessResource>(name);
        }
    }
}
