using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class VotePageDetails
    {
        public VotePageDetails(Vote voteIn, List<VoteOption> voteOptionsIn)
        {
            voteOptions = voteOptionsIn;
            vote = voteIn;
        }
        public VotePageDetails()
        {
            voteOptions = new List<VoteOption>();
            vote = new Vote();
        }
        public Vote vote { get; set; }
        public List<VoteOption> voteOptions { get; set; }
        public String SelectedButton { get; set; }
    }
}