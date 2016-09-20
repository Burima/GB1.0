using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using GB.Data.DBEntity;

namespace GBAdmin.Web.Models
{
    public class OwnerViewModel
    {
        public long OwnerDetailID { get; set; }
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
        public string Pincode { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string Notes { get; set; }
        public User User { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime LastUpdatedOn { get; set; }
        public long LastUpdatedBy { get; set; }
        [Display(Name = "City")]
        [Required]
        public int CityID { get; set; }
        [Display(Name = "FollowUp On")]
        public string FollowUpOn { get; set; }
        [Display(Name = "FollowUp Notes")]
        public string FollowUpNotes { get; set; }
        [Display(Name = "Next FollowUp")]
        public string NextFollowUp { get; set; }

        public List<OwnerDetail> OwnerDetailsList { get; set; }
        public virtual ICollection<OwnerDetailsActivityLog> OwnerDetailsActivityLogs { get; set; }
    }
}