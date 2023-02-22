using System;
using System.Collections.Generic;
using Volo.Abp.Identity;

namespace BaseService.Systems.UserManagement.Dto
{
    public class BaseIdentityUserDto: IdentityUserDto
    {
        public List<Guid> JobIds { get; set; }

        public List<Guid> OrganizationIds { get; set; }

        public List<string> RoleNames { get; set; }

        #region   >扩展字段<

        public string OrganizationNames { get; set; }

        public string JobNames { get; set; }

        #endregion
    }
}
