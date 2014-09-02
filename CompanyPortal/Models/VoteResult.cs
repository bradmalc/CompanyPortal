using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Models
{
    public class VoteResult
    {
        public VoteResult()
        {
        }
        
        [Required]
        [Display(Name = "Option:")]
        public string OptionText { get; set; }
        public int VoteCount { get; set; }
        public String Percentage { get; set; }
    }
}