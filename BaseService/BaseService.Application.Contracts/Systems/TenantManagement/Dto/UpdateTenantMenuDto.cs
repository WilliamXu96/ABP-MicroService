using BaseService.Systems.UserRoleMenusManagement.Dto;
using System;
using System.Collections.Generic;

namespace BaseService.Systems.TenantManagement.Dto
{
    public class UpdateTenantMenuDto
    {
        public Guid TenantId { get; set; }

        public List<MenusTreeDto> MenuIds { get; set; }
    }
}
