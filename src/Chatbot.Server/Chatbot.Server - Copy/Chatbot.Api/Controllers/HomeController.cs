using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chatbot.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpContext.Response.Redirect("~/swagger/ui/index");
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
