using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Snippets.Models;

namespace Snippets.Controllers
{
    public class Snippet_snippetCollectionController : Controller
    {
        private SnippetsContext db = new SnippetsContext();

        // GET: Snippet_snippetCollection
        public ActionResult Index()
        {
            var model = new Collections_Snippet_CombinedModel
            {
                snippets = db.snippets.ToList(),
                collection = db.collections.ToList()
            };

            return View(db.snippet_collections.ToList());
        }

        // GET: Snippet_snippetCollection/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Snippet_snippetCollection snippet_snippetCollection = db.snippet_collections.Find(id);
            if (snippet_snippetCollection == null)
            {
                return HttpNotFound();
            }
            return View(snippet_snippetCollection);
        }

        // GET: Snippet_snippetCollection/Create
        public ActionResult Create()
        {
            
            
            return View();
        }

        // POST: Snippet_snippetCollection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID")] Snippet_snippetCollection snippet_snippetCollection)
        {

            
            if (ModelState.IsValid)
            {
                
                db.snippet_collections.Add(snippet_snippetCollection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(snippet_snippetCollection);
        }

        // GET: Snippet_snippetCollection/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Snippet_snippetCollection snippet_snippetCollection = db.snippet_collections.Find(id);
            if (snippet_snippetCollection == null)
            {
                return HttpNotFound();
            }
            return View(snippet_snippetCollection);
        }

        // POST: Snippet_snippetCollection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID")] Snippet_snippetCollection snippet_snippetCollection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(snippet_snippetCollection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(snippet_snippetCollection);
        }

        // GET: Snippet_snippetCollection/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Snippet_snippetCollection snippet_snippetCollection = db.snippet_collections.Find(id);
            if (snippet_snippetCollection == null)
            {
                return HttpNotFound();
            }
            return View(snippet_snippetCollection);
        }

        // POST: Snippet_snippetCollection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Snippet_snippetCollection snippet_snippetCollection = db.snippet_collections.Find(id);
            db.snippet_collections.Remove(snippet_snippetCollection);
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
