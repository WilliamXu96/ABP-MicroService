using Business.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
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

            var products = business.AddPermission(BusinessPermissions.DataDictionary.Default, L("DataDictionary"));
            products.AddChild(BusinessPermissions.DataDictionary.Update, L("Edit"));
            products.AddChild(BusinessPermissions.DataDictionary.Delete, L("Delete"));
            products.AddChild(BusinessPermissions.DataDictionary.Create, L("Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BusinessResource>(name);
        }
    }
}
