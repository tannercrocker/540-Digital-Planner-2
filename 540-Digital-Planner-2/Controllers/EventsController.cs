using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Digital_Planner.Models;

namespace Digital_Planner.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private ApplicationDbContext db = AccountController.GetNewDbContext();

        // GET: Users/Schedule/5
        public ActionResult Schedule(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Require logged in user
            //Get logged in user's events
            ApplicationUser user = AccountController.CurrentUser(User.Identity);

            if (user == null)
            {
                return HttpNotFound();
            }
            //The view manages the stuff it needs. Not proper MVC style, but eh, it works.
            return View(user);
        }

        // GET: Events
        public ActionResult Index()
        {
            ApplicationUser user = AccountController.CurrentUser(User.Identity);
            return View(user.getEventsWithCategories());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ApplicationUser user = AccountController.CurrentUser(User.Identity);
            ViewBag.MakeAvailabilities = false;
            if (user.getAvailabilities().Count() <= 0)
            {
                ViewBag.MakeAvailabilities = true;
            }
            ViewBag.MakeCategories = false;
            if (user.getCategories().Count() <= 0)
            {
                ViewBag.MakeCategories = true;
            }
            ViewBag.CategoryID = new SelectList(user.getCategories(), "CategoryID", "Description");
            return View(new Event() { UserID = user.Id });
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,AutoAssign,Title,OccursAt,Duration,Priority,CompleteBy,IsComplete,Location,UserID,CategoryID")] Event @event, int? recurrence)
        {
            ApplicationUser user = AccountController.CurrentUser(User.Identity);

            if (ModelState.IsValid)
            {
                @event.UserID = user.Id;
                db.Events.Add(@event);
                db.SaveChanges();

                if (recurrence != null)
                {
                    if (recurrence > 24)    //Prevents malicious users from locking up the system
                    {
                        recurrence = 24;
                    }
                    var Occurs = @event.OccursAt;
                    var Complete = @event.CompleteBy;
                    while (recurrence > 0)
                    {
                        var re_event = new Event();
                        Occurs = Occurs.AddDays(7);
                        Complete = Complete.AddDays(7);

                        re_event.OccursAt = Occurs;
                        re_event.CompleteBy = Complete;
                        re_event.IsComplete = @event.IsComplete;
                        re_event.Priority = @event.Priority;
                        re_event.Title = @event.Title;
                        re_event.UserID = @event.UserID;
                        re_event.CategoryID = @event.CategoryID;
                        re_event.Location = @event.Location;
                        re_event.AutoAssign = @event.AutoAssign;
                        re_event.Duration = @event.Duration;

                        db.Events.Add(re_event);
                        db.SaveChanges();

                        recurrence--;
                    }
                }

                Sorting.Planner.GenerateSchedule(AccountController.CurrentUser(User.Identity));
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(user.getCategories(), "CategoryID", "Description", @event.CategoryID);
            @event.UserID = user.Id;
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            ApplicationUser user = AccountController.CurrentUser(User.Identity);
            ViewBag.CategoryID = new SelectList(user.getCategories(), "CategoryID", "Description", @event.CategoryID);
            @event.UserID = user.Id;
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,Title,AutoAssign,OccursAt,Duration,Priority,CompleteBy,IsComplete,Location,UserID,CategoryID")] Event @event)
        {
            ApplicationUser user = AccountController.CurrentUser(User.Identity);

            if (ModelState.IsValid)
            {
                @event.UserID = user.Id;
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();

                Sorting.Planner.GenerateSchedule(AccountController.CurrentUser(User.Identity));
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(user.getCategories(), "CategoryID", "Description", @event.CategoryID);
            @event.UserID = user.Id;
            return View(@event);
        }

        // POST: Events/5
        // Changes the attribute of an event to the specified value
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ToggleCompletion(int? id)
        {
            if (id != null)
            {
                var evt = db.Events.Single(e => e.EventID == id);
                evt.IsComplete = !evt.IsComplete;
                db.Entry(evt);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            Boolean was_auto = @event.AutoAssign;
            db.Events.Remove(@event);
            db.SaveChanges();

            if(was_auto)
            {
                Sorting.Planner.GenerateSchedule(AccountController.CurrentUser(User.Identity));
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
