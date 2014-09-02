using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CompanyPortal.Models;

namespace CompanyPortal.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0 && Convert.ToInt32(Session["Admin"]) == 1)
                        {
                            int id = Convert.ToInt32(Session["id"]);
                            DBUtil db = new DBUtil();
                            Company c = new Company();
                            return View();
                        }
                        return Redirect("~/Home");
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "AdminIndex" });
                    }
                }
                else
                    ViewBag.Message = "You must login to manage your Account Information";
            }
            return Redirect("~/Home");
        }
        //Takes edited company info in from the browser and updates company in the database
        [HttpPost]
        public ActionResult EditCompany(Company company)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0 && Convert.ToInt32(Session["Admin"]) == 1)
                        {
                            DBUtil db = new DBUtil();
                            db.UpdateCompanyInfo(company);
                            return Redirect("~/Account");
                        }
                        return Redirect("~/Home");
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "EditCompany" });
                    }
                }
            }
            return Redirect("~/Home");
        }
        //get current company info and display it for the user to update
        public ActionResult EditCompany()
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            int companyID = (int)Session["CompanyID"];
                            DBUtil db = new DBUtil();
                            Company company = db.GetCompanyInfo(companyID);
                            return View(company);
                        }
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "EditCompany" });
                    }
                }
            }
            return Redirect("~/Home");
        }
        //takes in user populated user information and add the user to the db
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            try
            {
                DBUtil db = new DBUtil();
                User currUser = new User();
                currUser.UserID = Convert.ToInt32(Session["id"]);
                user.CompanyID = db.GetCompanyByUser(Convert.ToInt32(Session["id"]));
                int userID = db.InsertUser(user);
                if(userID != -1)
                {
                    return RedirectToAction("AddUserGroups", new { UserID = userID });
                }
                else
                {
                    return RedirectToAction("AddUser");
                }
            }
            catch (Exception ex)
            {
                //Log the error
                return RedirectToAction("ErrorPage", "Page", new { pageName = "AddUser" });
            }
        }
        //Create a new user object and pass it to the page for the user to populate
        public ActionResult AddUser()
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            User user = new User();
                            return View(user);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "AddUser" });
                    }
                }
            }
            return Redirect("~/Home");
        }
        //Displays the current user data to allow user to update it
        public ActionResult SelectUserEdit()
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            int id = (int)Session["id"];
                            DBUtil db = new DBUtil();
                            List<User> users = db.GetUsersForCompany(id);
                            return View(users);
                        }
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "EditUser" });
                    }
                }
            }
            return Redirect("~/Home");
        }

        //Displays the current user data to allow user to update it
        [HttpPost]
        public ActionResult UserEdit(User user)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0 && Convert.ToInt32(Session["Admin"]) == 1)
                        {
                            DBUtil db = new DBUtil();
                            db.UpdateUserInfo(user);
                            return Redirect("~/Company/EditUser");
                        }
                        return Redirect("~/Home");
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "UserEdit" });
                    }
                }
            }
            return Redirect("~/Home");
        }
        public ActionResult UserEdit(int userID)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0 && Convert.ToInt32(Session["Admin"]) == 1)
                        {
                            DBUtil db = new DBUtil();
                            User user = db.GetUserInfo(userID);
                            return View(user);
                        }
                        return Redirect("~/Home");
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "UserEdit" });
                    }
                }
            }
            return Redirect("~/Home");
        }
        public ActionResult AddVote()
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            Vote vote = new Vote();
                            List<SelectListItem> options = new List<SelectListItem>();
                            options.Add(new SelectListItem { Value = "2", Text = "Two" });
                            options.Add(new SelectListItem { Value = "3", Text = "Three" });
                            options.Add(new SelectListItem { Value = "4", Text = "Four" });
                            options.Add(new SelectListItem { Value = "5", Text = "Five" });
                            AddVotePage page = new AddVotePage();
                            page.options = options;
                            page.vote = vote;
                            return View(page);
                        }
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "AddVote" });
                    }
                }
            }
            return Redirect("~/Admin");
        }
        //takes in user populated user information and add the user to the db
        [HttpPost]
        public ActionResult AddVote(AddVotePage page)
        {
            try
            {
                DBUtil db = new DBUtil();
                page.vote.VoteCreator = Convert.ToInt32(Session["id"]);
                int voteID = db.AddVote(page.vote);
                if(voteID != -1)
                {
                    return RedirectToAction("AddVoteOptions", new { VoteID = voteID, OptionCount = page.optionCount });
                }
                else
                {
                    return RedirectToAction("AddVote");
                }

            }
            catch (Exception ex)
            {
                //log the error
                return RedirectToAction("ErrorPage", "Page", new { pageName = "AddVote" });
            }
        }
        public ActionResult AddVoteOptions(int voteID, int optionCount)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            AddVoteOptionPage page = new AddVoteOptionPage();
                            for(int i = 0; i < optionCount; i++)
                            {
                                page.options.Add(new VoteOption());
                            }
                            page.voteID = voteID;
                            return View(page);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Delete vote which was created
                        DBUtil db = new DBUtil();
                        db.DeleteVote(voteID);
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "AddVoteOptions" });
                    }
                }
            }
            return Redirect("~/Admin");
        }
        [HttpPost]
        public ActionResult AddVoteOptions(AddVoteOptionPage page)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            DBUtil db = new DBUtil();
                            db.InsertVoteOptions(page.options, page.voteID);

                            return RedirectToAction("AddVoteGroups", "Admin", new { voteID = page.voteID });
                        }
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "AddVoteOptions" });
                    }
                }
            }
            return Redirect("~/Admin");
        }
        public ActionResult AddVoteGroups(int voteID)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            List<Group> userGroups = new List<Group>();
                            DBUtil db = new DBUtil();
                            userGroups = db.GetUserGroups(Convert.ToInt32(Session["id"]));
                            AddVoteGroupsPage page = new AddVoteGroupsPage();
                            page.groups = userGroups;
                            page.voteID = voteID;
                            return View(page);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Delete vote which was created
                        DBUtil db = new DBUtil();
                        db.DeleteVote(voteID);
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "AddVoteGroups" });
                    }
                }
            }
            return Redirect("~/Admin");
        }
        [HttpPost]
        public ActionResult AddVoteGroups(AddVoteGroupsPage page)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            DBUtil db = new DBUtil();
                            db.InsertVoteUserGroups(page);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Delete vote which was created
                        DBUtil db = new DBUtil();
                        db.DeleteVote(page.voteID);
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "AddVoteGroups" });
                    }
                }
            }
            return Redirect("~/Admin");
        }
        public ActionResult AddUserGroups(int userID)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            List<Group> userGroups = new List<Group>();
                            DBUtil db = new DBUtil();
                            userGroups = db.GetUserGroups(Convert.ToInt32(Session["id"]));
                            AddUserPage page = new AddUserPage();
                            page.groups = userGroups;
                            page.userID = userID;
                            return View(page);
                        }
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "AddUserGroups" });
                    }
                }
            }
            return Redirect("~/Admin");
        }
        [HttpPost]
        public ActionResult AddUserGroups(AddUserPage page)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            DBUtil db = new DBUtil();
                            db.InsertUserUserGroups(page);
                        }
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "AddUserGroups" });
                    }
                }
            }
            return Redirect("~/Admin");
        }
    }
}
