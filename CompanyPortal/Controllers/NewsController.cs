using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CompanyPortal.Models;

namespace CompanyPortal.Controllers
{
    public class NewsController : Controller
    {
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
                            List<Post> posts = db.GetPostsByUser(id);
                            List<Vote> votes = db.GetVotesByUser(id);
                            return View(new MainPageObjects(posts, votes));
                        }
                        else
                        {
                            Response.Redirect("/Registration/LogIn");
                        }
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "NewsIndex" });
                    }
                }
                else
                    Response.Redirect("/Registration/LogIn");
            }
            return View();
        }
        public ActionResult VotePage(int VoteID)
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
                            Vote v = db.GetVoteByID(VoteID, Convert.ToInt32(Session["id"]));
                            List<VoteOption> options = db.GetVoteOptions(VoteID);
                            return View(new VotePageDetails(v, options));
                        }
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "VotePage" });
                    }
                }
            }
            return Redirect("~/Home");
        }
        [HttpPost]
        public ActionResult VotePage(VotePageDetails voptions)
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
                            Vote v = db.GetVoteByOptionID(Convert.ToInt32(voptions.SelectedButton));
                            bool alreadyVoted = db.CheckUserVote(v.VoteID, Convert.ToInt32(Session["id"]));
                            if(!alreadyVoted)
                            {
                                db.InsertUserVote(v, Convert.ToInt32(voptions.SelectedButton), Convert.ToInt32(Session["id"]));
                                return RedirectToAction("VoteResults", new { VoteID = v.VoteID });
                            }
                            else
                            {
                                return RedirectToAction("VoteResults", new { VoteID = v.VoteID });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "VotePage" });
                    }
                }
            }
            return Redirect("~/News");
        }

        public ActionResult VoteResults(int VoteID)
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
                            Vote v = db.GetVoteByID(VoteID, Convert.ToInt32(Session["id"]));
                            List<VoteResult> results = db.GetVoteResults(VoteID);
                            int answerCount = 0;
                            foreach (VoteResult vr in results)
                            {
                                answerCount += vr.VoteCount;
                            }
                            foreach (VoteResult vr in results)
                            {
                                double perc = (double)vr.VoteCount / (double)answerCount;
                                vr.Percentage = String.Format("Value: {0:P2}.", perc);
                            }
                            return View(new VoteResultsPage(v, results));
                        }
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        return RedirectToAction("ErrorPage", "Page", new { pageName = "VoteResults" });
                    }
                }
            }
            return Redirect("~/News");
        }
	}
}