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
            //TODO: delete this call to GenerateSchedule().  It's just a temporary test
            Sorting.Planner.GenerateSchedule("all");

            //Testing. Please remove this and related code.
            // IT WORKS! We can find the User by the name, since the name = email.
            ViewBag.isAuth = User.Identity.IsAuthenticated;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Digital Planner is a software for planning your events. You will be able to log in or register" +
                " and create events and categories for your planner. With each created event, you will be able to see the events" +
                " through the My Schedule tab in a daily, weekly, or monthly view.";

            return View();
        }
        
    }
}