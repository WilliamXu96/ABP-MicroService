using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace BaseService.Systems.UserManagement.Dto
{
    public class GetBaseIdentityUsersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public Guid? OrganizationId { get; set; }
    }
}
