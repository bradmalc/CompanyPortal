using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class Project
    {
        public string projectName { get; set; }
        public int projectID { get; set; }
        public List<Group> userGroups { get; set; }
    }
}