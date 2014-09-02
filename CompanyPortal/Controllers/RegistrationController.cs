/* Class: RegistrationController.cs 
 * Date: November 20, 2013
 * Purpose: Looks after the logging in and registration of companies
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity.Validation;
using CompanyPortal.Models;
using System.Data.SqlClient;

namespace CompanyPortal.Controllers
{
    public class RegistrationController : Controller
    {
        //Login page before filled out
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        //attempts to use the inputted login info to login the user
        [HttpPost]
        public ActionResult LogIn(User user)
        {
            try
            {
                if (IsValid(ref user))
                {
                    Session.Add("UserName", user.UserName);
                    Session.Add("id", user.UserID);
                    Session.Add("CompanyID", user.CompanyID);
                    DBUtil db = new DBUtil();
                    if (db.CheckAdmin(Convert.ToInt32(Session["id"])))
                    {
                        Session.Add("Admin", 1);
                        FormsAuthentication.SetAuthCookie(user.FirstName, false);
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        Session.Add("Admin", 0);
                        FormsAuthentication.SetAuthCookie(user.FirstName, false);
                        return RedirectToAction("Index", "News");
                    }
                }
                else
                {
                    Response.Write("<script>alert('" + "Login details are wrong." + "');</script>");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return View();
            }
        }
        //Goes to the blank registration page
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //takes in the company object transfers it 
        [HttpPost]
        public ActionResult Register(Company company, User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DBUtil db = new DBUtil();
                    User newUser = db.Register(company, user);
                    if (user.UserID > 0)
                    {
                        return RedirectToAction("Index", "Company");
                    }
                    else
                        ModelState.AddModelError("", "Issue registering Company");
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return View();
            }
            return View();
        }
        //Log the user out
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["id"] = null;
            Session["Admin"] = null;
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        //check to see if the company login info is valid and if so set the Session's id variable
        private bool IsValid(ref User user)
        {
            try
            {
                bool IsValid = false;
                DBUtil db = new DBUtil();
                db.Login(ref user);
                if (user.UserID > 0)
                {
                    IsValid = true;
                }
                else
                    IsValid = false;

                return IsValid;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
