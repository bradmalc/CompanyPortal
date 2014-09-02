using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class AddVoteOptionPage
    {
        public AddVoteOptionPage(){
            options = new List<VoteOption>();
        }
        public List<VoteOption> options { get; set; }
        public int voteID { get; set; }
    }
}