using System;
using System.Collections.Generic;
using Volo.Abp.Identity;

namespace BaseService.Systems.UserManagement.Dto
{
    public class BaseIdentityUserUpdateDto : IdentityUserUpdateDto
    {
        public List<Guid> OrganizationIds { get; set; }

        public List<Guid> JobIds { get; set; }
    }
}
