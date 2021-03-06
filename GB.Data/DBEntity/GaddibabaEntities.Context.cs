﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GaddibabaEntities : DbContext
    {
        public GaddibabaEntities()
            : base("name=GaddibabaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(i => i.Roles).WithMany(u => u.Users)
             .Map(m =>
             {
                 m.ToTable("UserRoles");
                 m.MapLeftKey("UserId");
                 m.MapRightKey("RoleId");
             });
        }
    
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<LicenceType> LicenceTypes { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<ReferPersonDetail> ReferPersonDetails { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<OwnerDetail> OwnerDetails { get; set; }
        public virtual DbSet<OwnerDetailsActivityLog> OwnerDetailsActivityLogs { get; set; }
        public virtual DbSet<DriverDetail> DriverDetails { get; set; }
        public virtual DbSet<DriverDetailsActivityLog> DriverDetailsActivityLogs { get; set; }
        public virtual DbSet<DriverStatus> DriverStatus { get; set; }
    }
}
