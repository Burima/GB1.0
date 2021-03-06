//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GB.Data.DBEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.UserClaims = new HashSet<UserClaim>();
            this.UserDetails = new HashSet<UserDetail>();
            this.UserLogins = new HashSet<UserLogin>();
            this.Roles = new HashSet<Role>();
            this.OwnerDetails = new HashSet<OwnerDetail>();
            this.OwnerDetailsActivityLogs = new HashSet<OwnerDetailsActivityLog>();
            this.DriverDetails = new HashSet<DriverDetail>();
            this.DriverDetailsActivityLogs = new HashSet<DriverDetailsActivityLog>();
        }
    
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBackGroundVerified { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime LastUpdatedOn { get; set; }
        public string ProfilePicture { get; set; }
        public Nullable<int> Gender { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int Status { get; set; }
        public long CreatedBy { get; set; }
        public long LastUpdatedBy { get; set; }
        public string PhoneVerificationCode { get; set; }
        public int CityID { get; set; }
        public int OrganizationID { get; set; }
    
        public virtual City City { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<OwnerDetail> OwnerDetails { get; set; }
        public virtual ICollection<OwnerDetailsActivityLog> OwnerDetailsActivityLogs { get; set; }
        public virtual ICollection<DriverDetail> DriverDetails { get; set; }
        public virtual ICollection<DriverDetailsActivityLog> DriverDetailsActivityLogs { get; set; }
    }
}
