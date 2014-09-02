using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class SuccessPage
    {
        public SuccessPage()
        {
            buttons = new List<PageButton>();
        }
        public string successMessage { get; set; }
        public List<PageButton> buttons { get; set; }
    }
}