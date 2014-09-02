using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class Group
    {
        public string groupName { get; set; }
        public int groupID { get; set; }
        public bool isChecked { get; set; } 
    }
}