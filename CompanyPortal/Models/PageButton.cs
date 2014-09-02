using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class PageButton
    {
        public PageButton(string text, string actionPage, string controllerPage)
        {
            Text = text;
            ActionPage = actionPage;
            ControllerPage = controllerPage;
        }
        public string Text { get; set; }
        public string ActionPage { get; set; }
        public string ControllerPage { get; set; }
    }
}