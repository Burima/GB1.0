using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBAdmin.Web.Services
{
    public class Constants
    {
        public enum Roles
        {
            SuperAdmin = 1,
            Admin = 2,
            Manager = 3,
            Employee = 4,
            Marketing = 5,
            Telecaller=6,
            Sales=7
        }
    }
}