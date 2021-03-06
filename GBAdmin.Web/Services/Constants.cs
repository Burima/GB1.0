﻿using System;
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
            //Sales=7
        }

        public enum EnumDriverStatus
        {
            New = 1,
            Approved = 2,
            Rejected = 3,
            HaveLicence = 4,
            AppliedForLicence = 5,
            LearnerReceived = 6,
            LicenceReceived = 7,
            Attached = 8,
            PartnerMatched = 9,
            CompletedFirstTrip = 10,
            Dormant = 11,
            
        }
        public enum Organizations
        {
            VS = 1,
            Uber = 2,            
        }
    }
}