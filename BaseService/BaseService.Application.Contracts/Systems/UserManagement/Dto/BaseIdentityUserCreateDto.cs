using System;
using System.Collections.Generic;
using Volo.Abp.Identity;

namespace BaseService.Systems.UserManagement.Dto
{
    public class BaseIdentityUserCreateDto : IdentityUserCreateDto
    {
        public List<Guid> OrganizationIds { get; set; }

        public List<Guid> JobIds { get; set; }
    }
}
