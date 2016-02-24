using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Snippets.Models;
using Microsoft.AspNet.Identity;

namespace Snippets.Controllers
{
    public class snippetCollectionsController : Controller
    {
        private SnippetsContext db = new SnippetsContext();

        // GET: snippetCollections
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            return View(db.collections.Where(x=>x.SubmitterUserId == userID));
        }
        public ActionResult partialCollections()
        {
            string userID = User.Identity.GetUserId();
            return PartialView(db.collections.Where(x => x.SubmitterUserId == userID));

        }

        // GET: snippetCollections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            snippetCollection snippetCollection = db.collections.Find(id);
            if (snippetCollection == null)
            {
                return HttpNotFound();
            }
            return View(snippetCollection);
        }

        // GET: snippetCollections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: snippetCollections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title")] snippetCollection snippetCollection)
        {
            if (ModelState.IsValid)
            {
                snippetCollection.SubmitterUserId = User.Identity.GetUserId();
                db.collections.Add(snippetCollection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(snippetCollection);
        }

        // GET: snippetCollections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            snippetCollection snippetCollection = db.collections.Find(id);
            if (snippetCollection == null)
            {
                return HttpNotFound();
            }
            return View(snippetCollection);
        }

        // POST: snippetCollections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,SubmitterUserId")] snippetCollection snippetCollection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(snippetCollection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(snippetCollection);
        }

        // GET: snippetCollections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            snippetCollection snippetCollection = db.collections.Find(id);
            if (snippetCollection == null)
            {
                return HttpNotFound();
            }
            return View(snippetCollection);
        }

        // POST: snippetCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            snippetCollection snippetCollection = db.collections.Find(id);
            db.collections.Remove(snippetCollection);
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
