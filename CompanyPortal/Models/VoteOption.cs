using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Models
{
    public class VoteOption
    {
        public VoteOption()
        {
        }
        public int VoteOptionID { get; set; }
        
        [Required]
        [Display(Name = "Option:")]
        public string OptionText { get; set; }
        public bool isChecked { get; set; } 
    }
}