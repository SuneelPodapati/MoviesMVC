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

        public ActionResult Design()
        {
            return View();
        }
        
        public ActionResult Index(string status)
        {
            var movies = new List<Movie>();
            try
            {
                ViewBag.status = status ?? "";
                movies = db.Movies.Include(m => m.Producer).ToList();
            }
            catch (Exception exp)
            {

            }
            return View(movies);
        }
        
        public ActionResult Create()
        {
            MovieViewModel viewModel = new MovieViewModel();
            try
            {
                viewModel.AllActors = new SelectList(db.Actors, nameof(Actor.ActorId), nameof(Actor.ActorName));
                viewModel.AllProducers = new SelectList(db.Producers, nameof(Producer.ProducerId), nameof(Producer.ProducerName));
            }
            catch (Exception exp)
            {

            }
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var allActorsList = db.Actors.ToList();
                    viewModel.Movie.Actors = allActorsList.Where(x => viewModel.SelectedMovieActorsIds.Contains(x.ActorId)).ToList();
                    db.Movies.Add(viewModel.Movie);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { status = "success" });
                }
                viewModel.AllActors = new MultiSelectList(db.Actors, nameof(Actor.ActorId), nameof(Actor.ActorName), viewModel.SelectedMovieActorsIds);
                viewModel.AllProducers = new SelectList(db.Producers, nameof(Producer.ProducerId), nameof(Producer.ProducerName), viewModel.Movie.ProducerId);
            }
            catch (Exception exp)
            {

            }
            return View(viewModel);
        }
        
        public ActionResult Edit(int? id)
        {
            MovieViewModel viewModel = new MovieViewModel();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                viewModel.Movie = db.Movies.Include(i => i.Actors).First(i => i.MovieId == id);
                if (viewModel.Movie == null)
                {
                    return HttpNotFound();
                }
                viewModel.SelectedMovieActorsIds = viewModel.Movie.Actors.Select(x => x.ActorId).ToList();
                viewModel.AllActors = new MultiSelectList(db.Actors, nameof(Actor.ActorId), nameof(Actor.ActorName), viewModel.SelectedMovieActorsIds);
                viewModel.AllProducers = new SelectList(db.Producers, nameof(Producer.ProducerId), nameof(Producer.ProducerName), viewModel.Movie.ProducerId);
            }
            catch (Exception exp)
            {
                
            }
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var allActorsList = db.Actors.ToList();
                    viewModel.Movie.Actors = allActorsList.Where(x => viewModel.SelectedMovieActorsIds.Contains(x.ActorId)).ToList();
                    var movie = db.Movies.Include(a => a.Actors).Single(x => x.MovieId == viewModel.Movie.MovieId);
                    var actors = db.Actors.Where(x => viewModel.SelectedMovieActorsIds.Contains(x.ActorId));
                    movie.Actors.Clear();
                    foreach (var item in actors)
                    {
                        movie.Actors.Add(item);
                    }
                    movie.MovieName = viewModel.Movie.MovieName;
                    movie.Plot = viewModel.Movie.Plot;
                    movie.Poster = viewModel.Movie.Poster;
                    movie.ProducerId = viewModel.Movie.ProducerId;
                    movie.YearOfRelease = viewModel.Movie.YearOfRelease;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { status = "success" });
                }
                viewModel.AllActors = new MultiSelectList(db.Actors, nameof(Actor.ActorId), nameof(Actor.ActorName), viewModel.SelectedMovieActorsIds);
                viewModel.AllProducers = new SelectList(db.Producers, nameof(Producer.ProducerId), nameof(Producer.ProducerName), viewModel.Movie.ProducerId);
            }
            catch (Exception exp)
            {
                
            }
            return View(viewModel);
        }

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
