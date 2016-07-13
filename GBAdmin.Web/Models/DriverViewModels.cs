using GB.Data.DBEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GBAdmin.Web.Models
{
    public class DriverViewModel
    {
        public long ID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Pincode")]
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Invalid pincode")]
        public string Pincode { get; set; }

        [Display(Name = "Licence Type")]
        [Required]
        public int LicenceType { get; set; }

        [Display(Name = "Licence No")]       
        public string LicenceNo { get; set; }

        [Display(Name = "Uber")]
        public bool Uber { get; set; }

        [Display(Name = "Ola")]
        public bool Ola { get; set; }
      
        public bool IsReferred { get; set; }
        public long UserID { get; set; }

        [Display(Name = "Expected Salary")]
        [Required]
        public string ExpectedSalary { get; set; }

        [Display(Name = "Driver Status")]
        public int Status { get; set; }

        public User User { get; set; }
        //public ReferPersonViewModel ReferPersonViewModel { get; set; }
        public List<DriverDetail> DriverDetailsList { get; set; }
    }

    public class ReferPersonViewModel
    {
        public long ReferPersonID { get; set; }
        public string Email { get; set; }

        public int PhoneNumber { get; set; }
    }
}