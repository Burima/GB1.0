using Microsoft.AspNet.Identity.EntityFramework;

namespace GB.Membership
{
    public class UserClaim : IdentityUserClaim<long>
    {

        public virtual User User { get; set; }
    }
}
