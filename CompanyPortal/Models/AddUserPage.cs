using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class AddUserPage
    {
        public int userID { get; set; }
        public List<Group> groups { get; set; }
    }
}