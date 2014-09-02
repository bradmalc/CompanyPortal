using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Models
{
    public class Post
    {
        public Post()
        {
        }
        public int PostID { get; set; }
        [Required]
        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }
        [Required]
        [Display(Name = "Post")]
        public string PostText { get; set; }
        [Required]
        [Display(Name = "Date:")]
        public string PostDate { get; set; }
        [Required]
        [Display(Name = "Posted By:")]
        public string UserName { get; set; }
    }
}