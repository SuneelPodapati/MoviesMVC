using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MMDB.Models;

namespace MMDB.Controllers
{
    public class ActorsController : Controller
    {
        private MMDBSQLEntities db = new MMDBSQLEntities();

        
        public ActionResult Index(string status)
        {
            var actors = new List<Actor>();
            try
            {
                ViewBag.status = status ?? "";
                actors = db.Actors.ToList();
            }
            catch (Exception exp)
            {

            }
            return View(actors);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActorId,ActorName,Gender,DateOfBirth,Bio")] Actor actor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Actors.Add(actor);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { status = "success" });
                }
            }
            catch (Exception exp)
            {
                
            }

            return View(actor);
        }
        
        public ActionResult Edit(int? id)
        {
            var actor = new Actor();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                actor = db.Actors.Find(id);
                if (actor == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception exp)
            {
                
            }
            return View(actor);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActorId,ActorName,Gender,DateOfBirth,Bio")] Actor actor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dbActor =  db.Actors.Single(x => x.ActorId == actor.ActorId);
                    dbActor.ActorName = actor.ActorName;
                    dbActor.Bio = actor.Bio;
                    dbActor.DateOfBirth = actor.DateOfBirth;
                    dbActor.Gender = actor.Gender;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { status = "success" });
                }
            }
            catch (Exception exp)
            {
                
            }
            return View(actor);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

      

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actor actor = db.Actors.Find(id);
            db.Actors.Remove(actor);
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
