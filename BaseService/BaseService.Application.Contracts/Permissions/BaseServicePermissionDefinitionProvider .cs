using BaseService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace BaseService.Permissions
{
    public class BaseServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {

        public override void Define(IPermissionDefinitionContext context)
        {
            var business = context.AddGroup(BaseServicePermissions.BaseService, L("BaseService"), MultiTenancySides.Tenant);

            var auditLogging = business.AddPermission(BaseServicePermissions.AuditLogging.Default, L("AuditLogging"));

            var dictionary = business.AddPermission(BaseServicePermissions.DataDictionary.Default, L("DataDictionary"));
            dictionary.AddChild(BaseServicePermissions.DataDictionary.Update, L("Edit"));
            dictionary.AddChild(BaseServicePermissions.DataDictionary.Delete, L("Delete"));
            dictionary.AddChild(BaseServicePermissions.DataDictionary.Create, L("Create"));

            var organization = business.AddPermission(BaseServicePermissions.Organization.Default,L("Organization"));
            organization.AddChild(BaseServicePermissions.Organization.Update, L("Edit"));
            organization.AddChild(BaseServicePermissions.Organization.Delete, L("Delete"));
            organization.AddChild(BaseServicePermissions.Organization.Create, L("Create"));

            var job = business.AddPermission(BaseServicePermissions.Job.Default, L("Job"));
            job.AddChild(BaseServicePermissions.Job.Update, L("Edit"));
            job.AddChild(BaseServicePermissions.Job.Delete, L("Delete"));
            job.AddChild(BaseServicePermissions.Job.Create, L("Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BaseServiceResource>(name);
        }
    }
}
