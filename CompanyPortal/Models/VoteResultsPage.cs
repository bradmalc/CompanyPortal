using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class VoteResultsPage
    {
        public VoteResultsPage(Vote voteIn, List<VoteResult> resultsIn)
        {
            ResultVote = voteIn;
            VoteResults = resultsIn;
        }
        public Vote ResultVote { get; set; }
        public List<VoteResult> VoteResults { get; set; }
    }
}