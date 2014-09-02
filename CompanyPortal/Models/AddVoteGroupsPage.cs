using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class AddVoteGroupsPage
    {
        public int voteID { get; set; }
        public List<Group> groups { get; set; }
    }
}