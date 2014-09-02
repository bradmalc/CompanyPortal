using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Models
{
    public class Company
    {
        [Required]
        [Display(Name = "Company:")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Address:")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "City:")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Province:")]
        public string Province { get; set; }
        [Required]
        [Display(Name = "Postal Code:")]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "Telephone:")]
        public string Telephone { get; set; }
        [Required]
        [Display(Name = "Created Date:")]
        public DateTime CreatedDate { get; set; }
        public int CompanyID { get; set; }
    }
}