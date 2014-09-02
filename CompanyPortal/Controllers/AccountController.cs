/* Class: AccountController.cs 
 * Date: November 20, 2013
 * Purpose: To allow the viewing and editing of the company and user information
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyPortal.Models;
using System.Web.Security;

namespace CompanyPortal.Controllers
{
    public class AccountController : Controller
    {
        //Main page that shows the company info and the info for the companies users
        public ActionResult Index()
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            int id = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
                            //bluth_webservice_client.Company company = bluth_webservice_client.CompanyProxy.GetCompanyInfo(id);
                            //bluth_webservice_client.GetUsersForCompanyResponse resp = bluth_webservice_client.CompanyProxy.GetUsersForCompany(id);
                            //CompanyAndUsers cp = new CompanyAndUsers();
                            //cp.company = company;
                            //cp.users = resp.Users;
                            //return View(cp);

                            //Get company/user data
                            return View();
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                        return Redirect("~/Home");
                    }
                }
                else
                    ViewBag.Message = "You must login to manage your Account Information";
            }
            return View();
        }
        //Takes edited company info in from the browser and updates company in the database
        /*[HttpPost]
        public ActionResult EditCompany(Company company)
        {
            try
            {
                String response = CompanyProxy.UpdateCompanyInfo(company);
                return Redirect("~/Account");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return View();
            }
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
                            int id = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
                            Company company = bluth_webservice_client.CompanyProxy.GetCompanyInfo(id);
                            return View(company);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                        return View();
                    }
                }
            }
            return View();
        }
        //takes in user populated user information and add the user to the db
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            try
            {
                int id = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
                user.CompanyID = id;
                String response = CompanyProxy.RegisterUser(user);
                return Redirect("~/Account");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return View();
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
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                        return View();
                    }
                }
            }
            return View();
        }
        //Update the user info with data coming from page
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            try
            {
                String response = CompanyProxy.UpdateUserInfo(user);
                return Redirect("~/Account");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return View();
            }
        }
        //Displays the current user data to allow user to update it
        public ActionResult EditUser(int userId)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            int id = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
                            GetUsersForCompanyResponse resp = CompanyProxy.GetUsersForCompany(id);
                            User userOut = new User();
                            foreach (bluth_webservice_client.User user in resp.Users)
                            {
                                if (user.ID == userId)
                                {
                                    userOut = user;
                                }
                            }
                            return View(userOut);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                        return View();
                    }
                }
            }
            return View();
        }
        //Takes in company username, id and new password from the form and updates the users password
        [HttpPost]
        public ActionResult ChangePassword(Company comp)
        {
            try
            {
                String response = CompanyProxy.ChangePassword(comp.ID, comp.Username, comp.Password);
                return Redirect("~/Account");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return View();
            }
        }
        //Gets current company's information from the database to allow the password to be changed
        public ActionResult ChangePassword()
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0)
                        {
                            int id = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
                            Company company = CompanyProxy.GetCompanyInfo(id);
                            company.Password = "";
                            return View(company);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                        return View();
                    }
                }
            }
            return View();
        }*/
    }
}