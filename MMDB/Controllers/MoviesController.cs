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
    public class MoviesController : Controller
    {
        private MMDBSQLEntities db = new MMDBSQLEntities();

        // GET: Movies
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Producer);
            return View(movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            MovieViewModel viewModel = new MovieViewModel();
            var allActorsList = db.Actors.ToList();
            viewModel.AllActors = allActorsList.Select(o => new SelectListItem
            {
                Text = o.ActorName,
                Value = o.ActorId.ToString()
            });
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerName");
            return View(viewModel);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //db.Movies.Add(viewModel.Movie);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerName", viewModel.Movie.ProducerId);
            return View(viewModel);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieViewModel viewModel = new MovieViewModel()
            {
                Movie = db.Movies.Include(i => i.Actors).First(i => i.MovieId == id)
            };

            if (viewModel.Movie == null)
                return HttpNotFound();

            var allActorsList = db.Actors.ToList();
            viewModel.AllActors = allActorsList.Select(o => new SelectListItem
            {
                Text = o.ActorName,
                Value = o.ActorId.ToString()
            });
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerName", viewModel.Movie.ProducerId);
            return View(viewModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerName", viewModel.Movie.ProducerId);
            return View(viewModel);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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
