using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyPortal.Models;

namespace CompanyPortal.Controllers
{
    public class PageController : Controller
    {
        //
        // GET: /Page/
        public ActionResult ErrorPage(string pageName)
        {
            try
            {
                ErrorPage errorPage = new ErrorPage();

                PageButton button = new PageButton("Hidden", "Index", "Admin");
                errorPage.buttons.Add(button);
                if (pageName == "AddUser")
                {
                    errorPage.errorMessage = "Issue Adding User";
                    button = new PageButton("Admin Page", "Index", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Add User", "AddUser", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "AdminIndex")
                {
                    errorPage.errorMessage = "Issue Accessing Admin Page";
                    button = new PageButton("Home", "Index", "Home");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Admin Page", "Index", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "EditCompany")
                {
                    errorPage.errorMessage = "Issue Editing Company";
                    button = new PageButton("Admin Page", "Index", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Edit Company", "EditCompany", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "SelectUserEdit")
                {
                    errorPage.errorMessage = "Issue Selecting User to Edit";
                    button = new PageButton("Admin Page", "Index", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Edit User", "SelectUserEdit", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "UserEdit")
                {
                    errorPage.errorMessage = "Issue Editing User";
                    button = new PageButton("Admin Page", "Index", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Edit User", "UserEdit", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "AddVote")
                {
                    errorPage.errorMessage = "Issue Adding Vote";
                    button = new PageButton("Admin Page", "Index", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Add Vote", "AddVote", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "AddVoteOptions")
                {
                    errorPage.errorMessage = "Issue Adding Vote Options";
                    button = new PageButton("Admin Page", "Index", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Add Vote", "AddVote", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "AddVoteGroups")
                {
                    errorPage.errorMessage = "Issue Adding Vote Groups";
                    button = new PageButton("Admin Page", "Index", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Add Vote", "AddVote", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "AddUserGroups")
                {
                    errorPage.errorMessage = "Issue Adding User Groups";
                    button = new PageButton("Admin Page", "Index", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Add User Group", "AddUserGroups", "Admin");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "NewsIndex")
                {
                    errorPage.errorMessage = "Issue Viewing News Page";
                    button = new PageButton("Home Page", "Index", "Home");
                    errorPage.buttons.Add(button);
                    button = new PageButton("News Page", "Index", "News");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "NewsIndex")
                {
                    errorPage.errorMessage = "Issue Adding Vote Options";
                    button = new PageButton("Home Page", "Index", "Home");
                    errorPage.buttons.Add(button);
                    button = new PageButton("News Page", "Index", "News");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "VotePage")
                {
                    errorPage.errorMessage = "Issue Viewing Vote Page";
                    button = new PageButton("News Page", "Index", "News");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                else if (pageName == "VoteResults")
                {
                    errorPage.errorMessage = "Issue Viewing Vote Results";
                    button = new PageButton("News Page", "Index", "News");
                    errorPage.buttons.Add(button);
                    button = new PageButton("Contact Administrator", "Contact", "Home");
                    errorPage.buttons.Add(button);
                }
                return View(errorPage);
            }
            catch(Exception ex)
            {
                DBUtil db = new DBUtil();
                if (Convert.ToInt32(Session["id"]) != 0)
                {
                    db.InsertUserError(Convert.ToInt32(Session["id"]), ex.Message, "PageController");
                }
                else
                {
                    db.InsertUserError(ex.Message, "PageController");
                }
                return null;
            }
        }

        public ActionResult SuccessPage(SuccessPage page)
        {
            return View(page);
        }
	}
}