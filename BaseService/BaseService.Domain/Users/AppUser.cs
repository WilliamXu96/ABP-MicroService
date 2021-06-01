using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Users;

namespace BaseService.Users
{
    //public class AppUser : FullAuditedAggregateRoot<Guid>, IUser
    //{
    //    #region Base properties

    //    /* These properties are shared with the IdentityUser entity of the Identity module.
    //     * Do not change these properties through this class. Instead, use Identity module
    //     * services (like IdentityUserManager) to change them.
    //     * So, this properties are designed as read only!
    //     */

    //    public virtual Guid? TenantId { get; private set; }

    //    public virtual string UserName { get; private set; }

    //    public virtual string Name { get; private set; }

    //    public virtual string Surname { get; private set; }

    //    public virtual string Email { get; private set; }

    //    public virtual bool EmailConfirmed { get; private set; }

    //    public virtual string PhoneNumber { get; private set; }

    //    public virtual bool PhoneNumberConfirmed { get; private set; }

    //    #endregion


    //    //public Guid OrgId { get; set; }

    //    //public bool Enable { get; set; }

    //    private AppUser()
    //    {

    //    }
    //}
}
