/* Class: CompanyAndUsers.cs 
 * Date: November 20, 2013
 * Purpose: Custom object that will allow the passing of all the users and company information for the company 
 * to the page
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class CompanyAndUsers
    {
        public List<User> users { get; set; }
        public Company company { get; set; }
        public int selectedUser { get; set; }
    }
}