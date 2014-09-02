using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Models
{
    public class AddVotePage
    {
        public Vote vote { get; set; }
        public List<SelectListItem> options { get; set; }
        [Required]
        [Display(Name = "Option Count")]
        public int optionCount { get; set; }
    }
}