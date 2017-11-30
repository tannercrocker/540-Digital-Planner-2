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
    public class CategoriesController : Controller
    {
        private DigitalPlannerDbContext db = new DigitalPlannerDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.Categories;
            return View(categories.ToList());

            /*
            DPUser dp = new DPUsersController().CurrentDPUser();
            if (dp  != null)
            {
                var categories = db.Categories.Where(u => u.DPUserID == dp.DPUserID);
                return View(categories.ToList());
            }
            else
            {
                return View(new List<Category>());
            }
            */
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email");
            return View(new Category());
            /*
            DPUser dp = new DPUsersController().CurrentDPUser();
            if (dp != null)
            {
                return View(new Category() { DPUserID = dp.DPUserID });
            }
            else
            {
                //We should really never get this case.
                ViewBag.DPUserID = new SelectList(db.DPUsers, "DPuserID", "FirstName");
                return View(new Category());
            }
            */
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,DPUserID")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", category.UserID);
            //ViewBag.DPUserID = new SelectList(db.DPUsers, "DPUserID", "FirstName", category.DPUserID);
            return View(category);
            /*
            int current_dp = new DPUsersController().CurrentDPUser();
            if (current_dp > 0)
            {
                ViewBag.DPUserID = current_dp;
                return View(category);
            }
            else
            {
                ViewBag.DPUserID = new SelectList(db.DPUsers, "DPUserID", "FirstName", category.DPUserID);
                return View(category);
            }
            */
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", category.UserID);
            //ViewBag.DPUserID = new SelectList(db.DPUsers, "DPUserID", "FirstName", category.DPUserID);
            return View(category);
            /*
            int current_dp = new DPUsersController().CurrentDPUser();
            if (current_dp > 0)
            {
                ViewBag.DPUserID = current_dp;
                return View(category);
            }
            else
            {
                ViewBag.DPUserID = new SelectList(db.DPUsers, "DPUserID", "FirstName", category.DPUserID);
                return View(category);
            }
            */
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,DPUserID")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", category.UserID);
            //ViewBag.DPUserID = new SelectList(db.DPUsers, "DPUserID", "FirstName", category.DPUserID);
            return View(category);
            /*
            int current_dp = new DPUsersController().CurrentDPUser();
            if (current_dp > 0)
            {
                ViewBag.DPUserID = current_dp;
                return View(category);
            }
            else
            {
                ViewBag.DPUserID = new SelectList(db.DPUsers, "DPUserID", "FirstName", category.DPUserID);
                return View(category);
            }
            */
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
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
