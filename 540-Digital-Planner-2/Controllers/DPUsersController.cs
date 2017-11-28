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
        public ActionResult Schedule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Require logged in user
            //Get logged in user's events
            DPUser dpuser = db.DPUsers.Find(id);

            if (dpuser == null)
            {
                return HttpNotFound();
            }
            
            return View(dpuser);

        }

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

        /*
         * TC - Disabling this because DPUsers are created with a corresponding Account.
        // GET: Users/Create
        public ActionResult Create()
        {
            return View(new DPUser());
        }
        */

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //NT - Matching Current Users Logged in to DPUser
        public int CurrentUser()
        {
            var currentUserID = User.Identity.GetUserId();
            var dpuser = db.DPUsers.Where(u => u.UserID.Equals(currentUserID));
            if(dpuser.Count() > 0)
            {
                return dpuser.First().DPUserID;
            }
            else
            {
                return -1;
            }
        }

        //TC - @Natrone, I don't think we need these, but you can be the judge of that.
        // ^^ TC - Commenting out.
        /*
        //User Registering For an Account
        //-------------------------------
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(DPUser U)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (db)  //db == new DigitalPlannerDbContext(), if that isn't what you need, try ApplicationDbContext()
                    {
                        db.DPUsers.Add(U);
                        db.SaveChanges();
                        ModelState.Clear();
                        U = null;
                        ViewBag.Message = "Registration Successful";
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Inocrrect Data");
                }
            }
            catch(DbEntityValidationException e)
            {
                foreach(var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"" +
                        "{1}\" " + "has the following validation errors:", eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);
                    foreach(var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View();
        }

        //Login For Account
        //Will need help
        //-----------------
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(DPUser U)
        {
            /*
            if (IsAuthentic(U.Email, U.Password))
            {
                FormsAuthentication.SetAuthCookie(U.Email, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Incorrect Login");
            }
            * /
            return View(U);
        }
        
        //Testing Validity of LogIn
        private bool IsAuthentic(string email, string password)
        {
            bool Valid = false;
            var emailList = db.Users.FirstOrDefault(e => e.Email == email);
            //var passwordList = db.Users.FirstOrDefault(p => p.Password == password);
            if(User != null)
            {
                if((emailList.Email == email))
                {
                    Valid = true;
                }
            }
            return Valid;
        }

        //Logging Out
        //-----------
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        */
    }
}
