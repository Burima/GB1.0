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
        [System.ComponentModel.DataAnnotations.Key]
        public long DriverDetailsID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int LicenceTypeID { get; set; }
        public string LicenceNo { get; set; }
        public int ExperienceInKolkata { get; set; }
        public string Pincode { get; set; }
        public Nullable<bool> Uber { get; set; }
        public Nullable<bool> Ola { get; set; }
        public Nullable<bool> Tfs { get; set; }
        public bool IsReferred { get; set; }
        public long CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime LastUpdatedOn { get; set; }
        public long LastUpdatedBy { get; set; }
    
        public virtual LicenceType LicenceType { get; set; }
    }
}
