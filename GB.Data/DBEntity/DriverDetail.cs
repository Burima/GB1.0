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
    
    public partial class DriverDetail
    {
        public long DriverID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public int LicenceType { get; set; }
        public string LicenceNo { get; set; }
        public int ExperienceInKolkata { get; set; }
        public string Pincode { get; set; }
        public Nullable<bool> Uber { get; set; }
        public Nullable<bool> Ola { get; set; }
        public Nullable<bool> Tfs { get; set; }
    
        public virtual LicenceType LicenceType1 { get; set; }
    }
}
