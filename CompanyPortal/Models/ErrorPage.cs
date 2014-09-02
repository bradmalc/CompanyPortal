using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class ErrorPage
    {
        public ErrorPage()
        {
            buttons = new List<PageButton>();
        }
        public string errorMessage { get; set; }
        public List<PageButton> buttons { get; set; }
    }
}