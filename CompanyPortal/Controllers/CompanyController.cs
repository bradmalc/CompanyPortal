using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CompanyPortal.Models;

namespace CompanyPortal.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/
        public ActionResult Index()
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (Convert.ToInt32(Session["id"]) != 0 && Session["id"] != null)
                        {
                            int id = Convert.ToInt32(Session["id"]);
                            DBUtil db = new DBUtil();
                            User user = new User();
                            user.UserID = id;
                            user.CompanyID = db.GetCompanyByUser(user.UserID);
                            Company comp = db.GetCompanyInfo(user.CompanyID);
                            return View(comp);
                        }
                        else
                        {
                            Response.Redirect("/Registration/LogIn");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                        return Redirect("~/Home");
                    }
                }
                else
                    Response.Redirect("/Registration/LogIn");
            }
            return View();
        }
        
	}
}