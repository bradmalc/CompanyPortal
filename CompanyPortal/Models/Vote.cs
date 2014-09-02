using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Models
{
    public class Vote
    {
        public Vote()
        {
        }
        public int VoteID { get; set; }
        [Required]
        [Display(Name = "Vote Name")]
        public string VoteName { get; set; }
        [Required]
        [Display(Name = "Question")]
        public string VoteQuestion { get; set; }
        [Required]
        [Display(Name = "Vote Date")]
        [DataType(DataType.Date)]
        public string VoteDate { get; set; }
        [Required]
        [Display(Name = "Vote End Date")]
        public string EndDate{ get; set; }
        public int VoteCreator { get; set; }
        public string DaysRemaining { get; set; }

        public List<VoteOption> options { get; set; }
    }
}