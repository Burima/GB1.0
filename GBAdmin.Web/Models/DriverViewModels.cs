using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GBAdmin.Web.Models
{
    public class DriverViewModel
    {
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        public int PhoneNumber { get; set; }
        [Display(Name = "Pincode")]
        [Required]
        public string Pincode { get; set; }
        [Display(Name = "Licence Type")]
        [Required]
        public int LicenceType { get; set; }
        [Display(Name = "Licence No")]
        [Required]
        public string LicenceNo { get; set; }
        [Display(Name = "Experience In Kolkata")]
        [Required]
        public int ExperienceInKolkata { get; set; }
        [Display(Name = "Uber")]
        public bool Uber { get; set; }
        [Display(Name = "Ola")]
        public bool Ola { get; set; }
        [Display(Name = "Tfs")]
        public bool Tfs { get; set; }
        public bool IsReferred { get; set; }
        public long UserID { get; set; }

        //public ReferPersonViewModel ReferPersonViewModel { get; set; }
    }

    public class ReferPersonViewModel
    {
        public long ReferPersonID { get; set; }
        public string Email { get; set; }

        public int PhoneNumber { get; set; }
    }
}