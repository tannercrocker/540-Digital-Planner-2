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
            int current_dp = new DPUsersController().CurrentDPUserID();
            if (current_dp > 0)
            {
                var categories = db.Categories.Where(u => u.DPUserID == current_dp);
                return View(categories.ToList());
            }
            else
            {
                return View(new List<Category>());
            }
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

            int current_dp = new DPUsersController().CurrentDPUserID();
            if (current_dp > 0)
            {
                ViewBag.DPUserID = current_dp;
                return View(new Category());
            }
            else
            {
                ViewBag.DPUserID = new SelectList(db.DPUsers, "DPuserID", "FirstName");
                return View(new Category());
            }

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

            int current_dp = new DPUsersController().CurrentDPUserID();
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

            int current_dp = new DPUsersController().CurrentDPUserID();
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

            int current_dp = new DPUsersController().CurrentDPUserID();
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
