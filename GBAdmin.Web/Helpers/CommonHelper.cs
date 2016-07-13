using GB.Data.DBEntity;
using GBAdmin.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBAdmin.Web.Helpers
{
    public class CommonHelper
    {
        GaddibabaEntities GBContext = new GaddibabaEntities();
        List<long> EmployeeIDList = new List<long>();
        public List<DriverDetail> GetDriverDetailsByUserID(long UserID, string Role)
        {
            List<DriverDetail> driverDetailsList = new List<DriverDetail>();
            GetEmployeeByUserID(UserID, Role);
            foreach (var EmployeeID in EmployeeIDList.Distinct())
            {
                driverDetailsList.AddRange(GBContext.DriverDetails.Where(m => m.UserID == EmployeeID).ToList());
            }
            return driverDetailsList;
        }

        private List<User> GetEmployeeByUserID(long UserID, string Role)
        {
            List<User> EmployeeList = new List<User>();
            EmployeeList = GBContext.Users.Where(m => m.CreatedBy == UserID).ToList();
            if (!EmployeeIDList.Contains(UserID))
            {
                EmployeeIDList.Add(UserID);
            }
            EmployeeIDList.AddRange(EmployeeList.Select(m => m.UserID).ToList());
            if (Role.ToUpper() == Constants.Roles.Admin.ToString().ToUpper())
            {
                foreach (var Employee in new List<User>(EmployeeList))
                {
                    if (Employee != null && Employee.Roles.FirstOrDefault().Name.ToUpper() == Constants.Roles.Manager.ToString().ToUpper())
                    {
                        EmployeeList.AddRange(GetEmployeeByUserID(Employee.UserID, Employee.Roles.FirstOrDefault().Name));
                       
                    }
                }
            }
            return EmployeeList;
        }

        public User GetAdminByID(long CreatedBy)
        {
            var User = GBContext.Users.FirstOrDefault(p => p.CreatedBy == CreatedBy);
            if (User.Roles.FirstOrDefault().Name.ToUpper() == Constants.Roles.Manager.ToString().ToUpper())
            {
                User = GetAdminByID(User.CreatedBy);
            }
            return User;
        }
        
    }
}