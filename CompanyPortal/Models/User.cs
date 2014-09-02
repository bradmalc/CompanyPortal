using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Models
{
    public class User
    {
        public User()
        {
            UserGroups = new List<Group>();
        }
        [Required]
        [Display(Name = "User name:")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password:")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public List<Group> UserGroups { get; set; }
    }
}