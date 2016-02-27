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
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: snippetCollections
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            return View(db.collections.Where(x=>x.SubmitterUserId == userID));
        }
        public ActionResult Search(string query)
        {
            if (query == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<snippetCollection> listOfMatchedCollections = new List<snippetCollection>();
            foreach (snippetCollection collection in db.collections)
            {
                if (collection.Title.ToLower() == query.ToLower())
                {
                    listOfMatchedCollections.Add(collection);
                }
            }
            List<snippetCollection> collectionsFound = listOfMatchedCollections.Where(x => x.IsPublic).Distinct().ToList();

            return View(collectionsFound);
        }

         public ActionResult ManageCollection(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            snippetCollection snippet = db.collections.Find(id);
            List<Snippet> snippetsInCollection = snippet.snippets;
            if (snippet == null)
            {
                return HttpNotFound();
            }
            return View(snippet);
        }
        public ActionResult MakePublic(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            snippetCollection collection = db.collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            collection.IsPublic = true;
            db.SaveChanges();
            return RedirectToAction("Index");
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
        [Authorize]
       
        public ActionResult SaveCollection(string id ) // might be ID
        {
            if (ModelState.IsValid)
            {


                snippetCollection collection = db.collections.Find(Convert.ToInt32(id));
                // add +1 to collection save count
                collection.SaveCount += 1;
                snippetCollection newSnippet = new snippetCollection();
                newSnippet.snippets = creatSnippetListCopy(collection.snippets.ToList());
                newSnippet.Title = collection.Title;
                newSnippet.SubmitterUserId = User.Identity.GetUserId();
                db.collections.Add(newSnippet);
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        public List<Snippet> creatSnippetListCopy(List<Snippet> snippets)
        {
            List<Snippet> listCopy = new List<Snippet>();
            foreach (Snippet snippet in snippets)
            {

                Snippet newSnip = new Snippet();
                newSnip.Link = snippet.Link;
                newSnip.SnippetCollection = snippet.SnippetCollection;
                newSnip.description = snippet.description;
                db.snippets.Add(newSnip);
                db.SaveChanges();
                listCopy.Add(snippet);
            }
            return listCopy;
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

       

        public ActionResult addSnippetToCollection(int? id)
        {
          //make new model and put snippet collection and snippet in there  


            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            snippetCollection SnippetCollection = db.collections.Find(id);
            if (SnippetCollection == null)
            {
                return HttpNotFound();
            }
            return View(SnippetCollection);
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
            //check and see if the list is not null
            // if not null, find all the snippets in the db that are the exact match, and remove them from the DB
          

            snippetCollection snippetCollection = db.collections.Find(id);
            if (snippetCollection.snippets.Count<1)
            {
                foreach (Snippet snippet in snippetCollection.snippets)
                {
                    Snippet FoundSnippet = db.snippets.Find(snippet.ID);
                    db.snippets.Remove(FoundSnippet);
                }
            }
            
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
