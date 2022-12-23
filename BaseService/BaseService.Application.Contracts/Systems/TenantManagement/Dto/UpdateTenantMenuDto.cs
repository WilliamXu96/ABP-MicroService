using System;
using System.Collections.Generic;

namespace BaseService.Systems.TenantManagement.Dto
{
    public class UpdateTenantMenuDto
    {
        public Guid TenantId { get; set; }

        public List<Guid> MenuIds { get; set; }
    }
}
