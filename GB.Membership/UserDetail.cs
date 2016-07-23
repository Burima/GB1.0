using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GB.Membership
{
    public class UserDetail
    {
        [Key]
        public int UserDetailID { get; set; }
        public long UserID { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public Nullable<int> GovtIDType { get; set; }
        public string GovtID { get; set; }
        public Nullable<int> UserProfession { get; set; }
        public string CurrentEmployer { get; set; }
        public string OfficeLocation { get; set; }
        public string EmployeeID { get; set; }
        public string HighestEducation { get; set; }
        public string InstitutionName { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime LastUpdatedOn { get; set; }
        public long LastUpdatedBy { get; set; }
        public long CreatedBy { get; set; }

        public virtual User User { get; set; }
    }
}
