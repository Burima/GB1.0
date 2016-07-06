using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBAdmin.Web.Models
{
    public class DashboardViewModel
    {
        public IDictionary<string, int> AttachedToUber { get; set; }
        public IDictionary<string, int> New { get; set; }
        public IDictionary<string, int> Approved { get; set; }
        public IDictionary<string, int> Rejected { get; set; }

    }
}