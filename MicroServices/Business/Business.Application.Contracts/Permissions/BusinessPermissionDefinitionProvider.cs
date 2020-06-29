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

            var dictionary = business.AddPermission(BusinessPermissions.DataDictionary.Default, L("DataDictionary"));
            dictionary.AddChild(BusinessPermissions.DataDictionary.Update, L("Edit"));
            dictionary.AddChild(BusinessPermissions.DataDictionary.Delete, L("Delete"));
            dictionary.AddChild(BusinessPermissions.DataDictionary.Create, L("Create"));

            var dictionaryDetail = business.AddPermission(BusinessPermissions.DataDictionaryDetail.Default, L("DataDictionary"));
            dictionaryDetail.AddChild(BusinessPermissions.DataDictionaryDetail.Update, L("Edit"));
            dictionaryDetail.AddChild(BusinessPermissions.DataDictionaryDetail.Delete, L("Delete"));
            dictionaryDetail.AddChild(BusinessPermissions.DataDictionaryDetail.Create, L("Create"));

            var organization = business.AddPermission(BusinessPermissions.Organization.Default,L("Organization"));
            organization.AddChild(BusinessPermissions.Organization.Update, L("Edit"));
            organization.AddChild(BusinessPermissions.Organization.Delete, L("Delete"));
            organization.AddChild(BusinessPermissions.Organization.Create, L("Create"));

            var job = business.AddPermission(BusinessPermissions.Job.Default, L("Job"));
            job.AddChild(BusinessPermissions.Job.Update, L("Edit"));
            job.AddChild(BusinessPermissions.Job.Delete, L("Delete"));
            job.AddChild(BusinessPermissions.Job.Create, L("Create"));

            var employee = business.AddPermission(BusinessPermissions.Employee.Default, L("Employee"));
            employee.AddChild(BusinessPermissions.Employee.Update, L("Edit"));
            employee.AddChild(BusinessPermissions.Employee.Delete, L("Delete"));
            employee.AddChild(BusinessPermissions.Employee.Create, L("Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BusinessResource>(name);
        }
    }
}
