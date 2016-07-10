using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBAdmin.Web.Models
{
    public class DashboardViewModel
    {
        
        public int New { get; set; }
        public int InProgress { get; set; }
        public int Rejected { get; set; }
        public int AttachedToUber { get; set; }

        public int Total { get; set; }

    }
}