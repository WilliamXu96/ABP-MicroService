using Business.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace FileStorage.Permissions
{
    public class StoragePermissionDefinitionProvider: PermissionDefinitionProvider
    {

        public override void Define(IPermissionDefinitionContext context)
        {
            var storage = context.AddGroup(StoragePermissions.StorageManagement, L("StorageManagement"));

            var file = storage.AddPermission(StoragePermissions.File.Default, L("File"));
            file.AddChild(StoragePermissions.File.Update, L("Edit"));
            file.AddChild(StoragePermissions.File.Delete, L("Delete"));
            file.AddChild(StoragePermissions.File.Create, L("Create"));

            //Code generation...
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<StorageResource>(name);
        }
    }
}
