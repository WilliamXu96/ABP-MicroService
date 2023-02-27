using System.Collections.Generic;

namespace BaseService.Systems.UserRoleMenusManagement.Dto
{
    public class UserMenuPermissionsDto
    {
        public string Tenant { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public List<string> MenuPermissions { get; set; }
    }
}
