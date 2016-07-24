using System;
using System.ComponentModel.DataAnnotations;

namespace GBAdmin.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "Password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string LoginError { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string RegisterError { get; set; }

        [Display(Name = "Role")]
        [Required]       
        public string RoleName { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"[789][0-9]{9}", ErrorMessage = "Entered Mobile No is not valid.")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityID { get; set; }
    }

    public class ResetPasswordViewModel
    {

        public string UserID { get; set; }
        public string Code { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class AccountViewModel
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
        public ResetPasswordViewModel ResetPasswordViewModel { get; set; }
        public ExternalLoginConfirmationViewModel ExternalLoginConfirmationViewModel { get; set; }

    }


    public class UserViewModel
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public bool IsBackGroundVerified { get; set; }
        public string ProfilePicture { get; set; }
        public Nullable<int> Gender { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"[789][0-9]{9}", ErrorMessage = "Entered Mobile No is not valid.")]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string IsVSEmployee { get; set; }
        [Display(Name = "Present Address")]
        public string PresentAddress { get; set; }
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }
        [Display(Name = "Government ID Type")]
        public Nullable<int> GovtIDType { get; set; }

        [Display(Name = "Government ID Number")]
        public string GovtID { get; set; }
        [Display(Name = "Highest Education")]
        public string HighestEducation { get; set; }
        [Display(Name = "Institution Name")]
        public string InstitutionName { get; set; }
        public int Status { get; set; }
        [Required]
        [Display(Name = "City")]
        public int CityID { get; set; }
    
        public ManageUserViewModel ManageUserViewModel { get; set; }

    }
}
