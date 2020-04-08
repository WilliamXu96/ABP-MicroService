using Business.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Business.Permissions
{
    public class BusinessPermissionDefinitionProvider: PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var business = context.AddGroup(BusinessPermissions.Business);

            var products = business.AddPermission(BusinessPermissions.DataDictionary.Default);
            products.AddChild(BusinessPermissions.DataDictionary.Update);
            products.AddChild(BusinessPermissions.DataDictionary.Delete);
            products.AddChild(BusinessPermissions.DataDictionary.Create);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BusinessResource>(name);
        }
    }
}
