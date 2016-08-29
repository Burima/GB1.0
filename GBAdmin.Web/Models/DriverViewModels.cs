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
        [RegularExpression(@"[789][0-9]{9}", ErrorMessage = "Entered Mobile No is not valid.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Pincode")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Invalid pincode")]
        //[RegularExpression(@"[47][0-9]{5}", ErrorMessage = "Entered pincode is not valid.")]
        public string Pincode { get; set; }

        [Display(Name = "License Type")]
        [Required]
        public int LicenceType { get; set; }

        [Display(Name = "License No")]       
        public string LicenceNo { get; set; }

        [Display(Name = "Uber")]
        public bool Uber { get; set; }

        [Display(Name = "Ola")]
        public bool Ola { get; set; }
      
        public bool IsReferred { get; set; }
        public long UserID { get; set; }

        [Display(Name = "Expected Salary")]
        public string ExpectedSalary { get; set; }

        [Display(Name = "Driver Status")]
        public int Status { get; set; }

        public User User { get; set; }
        
        [Display(Name = "City")]
        [Required]
        public int CityID { get; set; }

        [Display(Name = "FollowUp On")]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime FollowUpOn { get; set; }
        [Display(Name = "FollowUp Notes")]
        public string FollowUpNotes { get; set; }
        [Display(Name = "Next FollowUp")]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime NextFollowUp { get; set; }
        public Nullable<System.DateTime> AttachedOn { get; set; }
        public Nullable<System.DateTime> PartnerMatchedOn { get; set; }
        public Nullable<System.DateTime> CompletedFirstTripOn { get; set; }
    
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