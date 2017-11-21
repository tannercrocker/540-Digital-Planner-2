﻿using System;
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
    public class AvailabilitiesController : Controller
    {
        private DigitalPlannerDbContext db = new DigitalPlannerDbContext();

        // GET: Days
        public ActionResult Index()
        {
            var avails = db.Availabilities.Include(d => d.DPUser);
            return View(avails.ToList());
        }

        // GET: Days/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availability day = db.Availabilities.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return View(day);
        }

        // GET: Days/Create
        public ActionResult Create()
        {
            ViewBag.DPUserID = new SelectList(db.DPUsers, "ID", "FirstName");
            return View();
        }

        // POST: Days/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OccursAt,Duration,DPUserID")] Availability avail, int? recurrence)
        {

            if (ModelState.IsValid)
            {
                db.Availabilities.Add(avail);
                db.SaveChanges();

                if (recurrence != null)
                {
                    if (recurrence > 24)    //Prevents malicious users from locking up the system
                    {
                        recurrence = 24;
                    }
                    var day_to_use = avail.OccursAt;
                    while (recurrence > 0)
                    {
                        var re_day = new Availability();
                        day_to_use = day_to_use.AddDays(7);

                        re_day.OccursAt = day_to_use;
                        re_day.Duration = avail.Duration;
                        re_day.DPUserID = avail.DPUserID;

                        db.Availabilities.Add(re_day);
                        db.SaveChanges();

                        recurrence--;
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.DPUserID = new SelectList(db.DPUsers, "ID", "FirstName", avail.DPUserID);
            return View(avail);
        }

        // GET: Days/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availability avail = db.Availabilities.Find(id);
            if (avail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DPUserID = new SelectList(db.DPUsers, "ID", "FirstName", avail.DPUserID);
            return View(avail);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OccursAt,Duration,DPUserID")] Availability avail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DPUserID = new SelectList(db.DPUsers, "ID", "FirstName", avail.DPUserID);
            return View(avail);
        }

        // GET: Days/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availability avail = db.Availabilities.Find(id);
            if (avail == null)
            {
                return HttpNotFound();
            }
            return View(avail);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Availability avail = db.Availabilities.Find(id);
            db.Availabilities.Remove(avail);
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