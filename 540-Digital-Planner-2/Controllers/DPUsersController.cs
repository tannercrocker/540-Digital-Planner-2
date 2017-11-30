using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Digital_Planner.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Digital_Planner.Controllers
{
    [Authorize]
    public class DPUsersController : Controller
    {
        private DigitalPlannerDbContext db = new DigitalPlannerDbContext();

        // GET: Users
        /* TC - For Privacy and Security reasons, I'm removing this action from the controller
        public ActionResult Index()
        {
            return View(db.DPUsers.ToList());
        }
        */

        // GET: Users/Schedule/5
        public ActionResult Schedule(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Require logged in user
            //Get logged in user's events
            using (var new_db = new ApplicationDbContext())
            {
                ApplicationUser user = new_db.Users.Find(User.Identity.Name);

                if (user == null)
                {
                    return HttpNotFound();
                }

                //return View(db.Events.Where(e => e.UserID.Equals(user.Id)).ToList());
                return View(user);
            }
        }
        /*

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DPUser dpuser = db.DPUsers.Find(id);
            if (dpuser == null)
            {
                return HttpNotFound();
            }
            return View(dpuser);
        }
        */

        /*
         * TC - Disabling this because DPUsers are created with a corresponding Account.
        // GET: Users/Create
        public ActionResult Create()
        {
            return View(new DPUser());
        }
        */
        /*
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName")] DPUser dpuser)
        {
            if (ModelState.IsValid)
            {
                db.DPUsers.Add(dpuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dpuser);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DPUser dpuser = db.DPUsers.Find(id);
            if (dpuser == null)
            {
                return HttpNotFound();
            }
            return View(dpuser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName")] DPUser dpuser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dpuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dpuser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DPUser dpuser = db.DPUsers.Find(id);
            if (dpuser == null)
            {
                return HttpNotFound();
            }
            return View(dpuser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DPUser dpuser = db.DPUsers.Find(id);
            foreach (var user_event in dpuser.Events.ToList())
            {
                db.Events.Remove(user_event);
            }
            foreach (var category in dpuser.Categories.ToList())
            {
                db.Categories.Remove(category);
            }
            db.DPUsers.Remove(dpuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        */

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*
        #region Helpers
        //NT - Matching Current Users Logged in to DPUser
        //TC - Return null if there isn't a logged in user 
        //  (Things are reaaly messed up if that happens. 
        //      This only gets called from an authorized user.)
        [Authorize]
        public DPUser CurrentDPUser()
        {
            var currentUserID = User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id.Equals(currentUserID));
            if(user.Count() > 0)
             {
                 return user.First().DPUser;
             }
             else
             {
                 return null;
             }
        }
        #endregion
        */
    }
}
