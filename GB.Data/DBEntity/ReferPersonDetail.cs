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
    
    public partial class ReferPersonDetail
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long ReferPersonID { get; set; }
        public string EmailID { get; set; }
        public int PhoneNumber { get; set; }
    }
}
