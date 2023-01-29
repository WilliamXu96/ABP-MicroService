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
            var Business = context.AddGroup(BusinessPermissions.Business, L("Business"));

            var Book = Business.AddPermission(BusinessPermissions.Book.Default, L("Book"));
            Book.AddChild(BusinessPermissions.Book.Update, L("Edit"));
            Book.AddChild(BusinessPermissions.Book.Delete, L("Delete"));
            Book.AddChild(BusinessPermissions.Book.Create, L("Create"));

            var PrintTemplate = Business.AddPermission(BusinessPermissions.PrintTemplate.Default, L("PrintTemplate"));
            PrintTemplate.AddChild(BusinessPermissions.PrintTemplate.Update, L("Edit"));
            PrintTemplate.AddChild(BusinessPermissions.PrintTemplate.Delete, L("Delete"));
            PrintTemplate.AddChild(BusinessPermissions.PrintTemplate.Create, L("Create"));

            //Code generation...
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BusinessResource>(name);
        }
    }
}
