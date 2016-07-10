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
        public List<DriverDetail> GetDriverDetailsByUserID(long UserID, string Role)
        {
            List<DriverDetail> driverDetailsList = new List<DriverDetail>();
            List<User> EmployeeList = GetEmployeeByUserID(UserID, Role);
            foreach (var Employee in EmployeeList)
            {
                driverDetailsList.AddRange(GBContext.DriverDetails.Where(m => m.CreatedBy == Employee.UserID).ToList());
            }
            return driverDetailsList;
        }

        private List<User> GetEmployeeByUserID(long UserID, string Role)
        {
            List<User> EmployeeList = new List<User>();
            EmployeeList = GBContext.Users.Where(m => m.CreatedBy == UserID).ToList();

            if (Role.ToUpper() == Constants.Roles.Admin.ToString().ToUpper())
            {
                foreach (var Employee in EmployeeList)
                {
                    if (Employee.Roles.FirstOrDefault().Name == Constants.Roles.Manager.ToString().ToUpper())
                    {
                        EmployeeList.AddRange(GetEmployeeByUserID(Employee.UserID, Employee.Roles.FirstOrDefault().Name));
                    }
                }
            }
            return EmployeeList;
        }
        
    }
}