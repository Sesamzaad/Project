using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication4.Models;
using AjaxControlToolkit;
using System.Web.Security;

namespace MvcApplication4.Controllers
{
    public class gymCalendarController : Controller
    {
        private Calendar.CalendarDBContext db = new Calendar.CalendarDBContext();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View(db.CalendarEvents.ToList());
        }

        public ActionResult Validation()
        {
            var v = new Admin();
            return View(v);
        }
        [HttpPost]
        public ActionResult Validation(Admin admin, string returnurl)
        {
            if (ModelState.IsValid)
            {
                if (admin.password == "docent")
                {
                    FormsAuthentication.SetAuthCookie("docent", true);
                    return RedirectToAction("Admin");
                }
                else
                {

                    ViewBag.wrong = "You don't belong here!";
                    return RedirectToAction("Admin");

                }

            }
            return RedirectToAction("Index");
            
        }
        [Authorize]
        public ActionResult Admin()
        {
            
                return View(db.CalendarEvents.ToList());
  
        }
        [HttpPost]
        public ActionResult Admin(Calendar calendar)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Overview()
        {
            return View(db.CalendarEvents.ToList());
        }
        [HttpPost]
        public ActionResult Overview(Calendar calendar)
        {
               return RedirectToAction("Index");
        }
        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            Calendar calendar = db.CalendarEvents.Find(id);
            if (calendar == null)
            {
                return HttpNotFound();
            }
            return View(calendar);
        }

        //
        // GET: /Default1/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(Calendar calendar)
        {
            if (ModelState.IsValid)
            {

                    if (calendar.einde > calendar.start && calendar.einde != calendar.start)
                    {
                        if (calendar.titel != null && calendar.modus != null && calendar.note != null && calendar.opdrachten != null)
                        {
                            db.CalendarEvents.Add(calendar);
                            db.SaveChanges();
                            return RedirectToAction("Admin");
                        }
                        else
                        {
                            ViewBag.error = "All fiels required!";
                        }
                    }
                    else
                    {
                        ViewBag.error = "Event duration error!";
                    }

            }

            return View(calendar);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Calendar calendar = db.CalendarEvents.Find(id);
            if (calendar == null)
            {
                return HttpNotFound();
            }
            return View(calendar);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(Calendar calendar)
        {
            if (ModelState.IsValid)
            {
                if (calendar.einde > calendar.start && calendar.einde != calendar.start)
                {
                    db.Entry(calendar).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Admin");
                }
                else
                {
                        ViewBag.error = "Event duration error!";
                }
            }
            return View(calendar);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Calendar calendar = db.CalendarEvents.Find(id);
            db.CalendarEvents.Remove(calendar);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}