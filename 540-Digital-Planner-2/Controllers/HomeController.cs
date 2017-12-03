using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Digital_Planner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Digital Planner is a software for planning your events. You will be able to log in or register" +
                " and create events and categories for your planner. With each created event, you will be able to see the events" +
                " through the My Schedule tab in a daily, weekly, or monthly view.";

            return View();
        }
        
        public ActionResult Contact()
        {
            return View();
        }
    }
}