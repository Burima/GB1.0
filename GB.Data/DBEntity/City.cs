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
    
    public partial class City
    {
        public City()
        {
            this.Users = new HashSet<User>();
            this.OwnerDetails = new HashSet<OwnerDetail>();
            this.OwnerDetailsActivityLogs = new HashSet<OwnerDetailsActivityLog>();
            this.DriverDetails = new HashSet<DriverDetail>();
            this.DriverDetailsActivityLogs = new HashSet<DriverDetailsActivityLog>();
        }
    
        public int CityID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<OwnerDetail> OwnerDetails { get; set; }
        public virtual ICollection<OwnerDetailsActivityLog> OwnerDetailsActivityLogs { get; set; }
        public virtual ICollection<DriverDetail> DriverDetails { get; set; }
        public virtual ICollection<DriverDetailsActivityLog> DriverDetailsActivityLogs { get; set; }
    }
}
