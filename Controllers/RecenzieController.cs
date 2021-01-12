using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using scaffold.Models;

namespace scaffold.Controllers
{
    public class SearchFilter
    {
        public string Titlu { get; set; }
        public int Nota { get; set; }
    }

    public class RecenzieController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Recenzie
        [Route("Recenzie/AfisareRecenzii")]
        public ActionResult Index(string titlu, int? nota)
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }
            if (!string.IsNullOrEmpty(titlu))
            {
                if (!nota.ToString().Equals(""))
                {
                    return View(db.Recenzie.Where(x => x.Titlu == titlu && x.Nota == nota).OrderByDescending(x => x.Nota).ToList());
                }
                else
                {
                    return View(db.Recenzie.Where(x => x.Titlu == titlu).OrderByDescending(x => x.Nota).ToList());
                }
            }
            else if (!nota.ToString().Equals(""))
            {
                return View(db.Recenzie.Where(x => x.Nota == nota).OrderByDescending(x => x.Nota).ToList());
            }
            else
            {
                return View(db.Recenzie.ToList());
            }
        }

        // GET: Recenzie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recenzie recenzie = db.Recenzie.Find(id);
            if (recenzie == null)
            {
                return HttpNotFound();
            }
            recenzie.FilmData = db.Film.Find(recenzie.IDFilm);
            return View(recenzie);
        }

        // GET: Recenzie/Create
        public ActionResult Create()
        {
            Recenzie recenzie = new Recenzie();
            recenzie.FilmList = GetAllFilmTypes();
            return View(recenzie);
        }

        // POST: Recenzie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDRecenzie,Titlu,Nota,IDFilm")] Recenzie recenzie)
        {
            if (ModelState.IsValid)
            {
                db.Recenzie.Add(recenzie);
                db.SaveChanges();
                TempData["message"] = "Succesful creation";
                return RedirectToAction("Index");
            }

            return View(recenzie);
        }

        // GET: Recenzie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recenzie recenzie = db.Recenzie.Find(id);
            if (recenzie == null)
            {
                return HttpNotFound();
            }
            recenzie.FilmList = GetAllFilmTypes();
            return View(recenzie);
        }

        // POST: Recenzie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDRecenzie,Titlu,Nota,IDFilm")] Recenzie recenzie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recenzie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recenzie);
        }

        // GET: Recenzie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recenzie recenzie = db.Recenzie.Find(id);
            if (recenzie == null)
            {
                return HttpNotFound();
            }
            return View(recenzie);
        }

        // POST: Recenzie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recenzie recenzie = db.Recenzie.Find(id);
            db.Recenzie.Remove(recenzie);
            db.SaveChanges();
            TempData["message"] = "Succesful deletion";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Recenzie/Search")]
        public ActionResult Search()
        {
            SearchFilter request = new SearchFilter();
            return View(request);
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllFilmTypes()
        {
            var selectList = new List<SelectListItem>();

            foreach (var film in db.Film.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = film.IDFilm.ToString(),
                    Text = film.Denumire,
                });
            }
            return selectList;
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
