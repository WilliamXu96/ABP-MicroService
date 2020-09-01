using System;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.Identity
{
    public class GetIdentityUsersInput : PagedAndSortedResultRequestDto
    {
        public Guid? OrgId { get; set; }

        public string Filter { get; set; }
    }
}
