using System;
using System.Collections.Generic;

namespace BaseService.Systems.UserRoleMenusManagement.Dto
{
    public class UpdateRoleMenuDto
    {
        public Guid RoleId { get; set; }

        public List<Guid> MenuIds { get; set; }
    }
}
