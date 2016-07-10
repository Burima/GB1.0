using Microsoft.AspNet.Identity.EntityFramework;
namespace GB.Membership
{
    public class UserLogin : IdentityUserLogin<long>
    {
        public virtual User User { get; set; }
    }
}
