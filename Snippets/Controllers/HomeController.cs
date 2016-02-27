using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippets.Models;

namespace Snippets.Controllers
{
    public class HomeController : Controller
    {
        private SnippetsContext db = new SnippetsContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult mostSavedPartial()
        {
            
            return PartialView(db.collections.Where(m => m.IsPublic ==true).OrderByDescending(x => x.SaveCount));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}