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
using System.Drawing;
using System.Drawing.Imaging;

namespace Snippets.Controllers
{
    public class SnippetsController : Controller
    {
        
        private SnippetsContext db = new SnippetsContext();
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Snippets
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            return View(db.snippets.Where(x => x.SubmitterUserId == userID));
        }

        public ActionResult ManageCollection(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Snippet snippet = db.snippets.Find(id);
            if (snippet == null)
            {
                return HttpNotFound();
            }
            return View(snippet);
        }

        // GET: Snippets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Snippet snippet = db.snippets.Find(id);
            if (snippet == null)
            {
                return HttpNotFound();
            }
            return View(snippet);
        }

        //create Thubnail
        public static Bitmap CreateThumbnail(string url, int width, int height, int thumbWidth, int thumbHeight)
        {
            WebsiteThumbnail thumbnail = new WebsiteThumbnail(url, width, height, thumbWidth, thumbHeight);

            //Bitmap x = thumbnail.GetThumbnail();
            //System.IO.Stream test = System.IO.Stream(@"Images\image.jpg");
            ///x.Save("C:\\image.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //x.Dispose();
            return thumbnail.GetThumbnail();
        }
       public ActionResult extensionView(string id)
        {
            ExtensionInfo model = new ExtensionInfo { Url = id };
            return View(model);
        }

        // GET: Snippets/Create
        public ActionResult Create()
        {
            string userID = User.Identity.GetUserId();
            List<SelectListItem> collectionsForDropdown = new List<SelectListItem>();
            List<snippetCollection> collections = new List<snippetCollection>();
            collections = db.collections.Where(x => x.SubmitterUserId == userID).ToList();
            foreach (var collection in collections)
            {
                collectionsForDropdown.Add(new SelectListItem() { Text = collection.Title, Value = collection.ID.ToString() });
            }

            Collections_Snippet_CombinedModel model = new Collections_Snippet_CombinedModel
            {

                collection = db.collections.ToList(),
                collectionDropdown = collectionsForDropdown, 
                snippet = new Snippet()
            };
            return View(model);
        }
       

        // POST: Snippets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Link,description")] Snippet snippet, Collections_Snippet_CombinedModel collection)
        {
            if (ModelState.IsValid)
            {
                
                snippetCollection CurrentSnippetCollection = db.collections.Find(Convert.ToInt32( collection.selectedCollectionID));
                if (CurrentSnippetCollection == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                
                snippet.SnippetCollection = CurrentSnippetCollection;
                snippet.SubmitterUserId = User.Identity.GetUserId();
                CurrentSnippetCollection.snippets.Add(snippet);

                db.snippets.Add(snippet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));

                
            }

            return RedirectToAction ("Index");
        }
        
        public ActionResult ChromeCreate(string URL)
        {
            string userID = User.Identity.GetUserId();
            List<SelectListItem> collectionsForDropdown = new List<SelectListItem>();
            List<snippetCollection> collections = new List<snippetCollection>();
            
            collections = db.collections.Where(x => x.SubmitterUserId == userID).ToList();
            foreach (var collection in collections)
            {
                collectionsForDropdown.Add(new SelectListItem() { Text = collection.Title, Value = collection.ID.ToString() });
            }

            Collections_Snippet_CombinedModel model = new Collections_Snippet_CombinedModel
            {

                collection = db.collections.ToList(),
                collectionDropdown = collectionsForDropdown,
                url = URL,
                snippet = new Snippet()
            };

           
            return View(model);
        }
        [HttpPost]
        
        public ActionResult ChromeCreate( Collections_Snippet_CombinedModel collection)
        {
      

                snippetCollection CurrentSnippetCollection = db.collections.Find(Convert.ToInt32(collection.selectedCollectionID));
                if (CurrentSnippetCollection == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Snippet snippet = collection.snippet;
                snippet.SnippetCollection = CurrentSnippetCollection;
                snippet.SubmitterUserId = User.Identity.GetUserId();
                snippet.Link = collection.url;
                
                CurrentSnippetCollection.snippets.Add(snippet);
                

                db.snippets.Add(snippet);
                db.SaveChanges();
                return RedirectToAction("Index");

            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));


            }

            return RedirectToAction("Index");
        }
 

        // GET: Snippets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Snippet snippet = db.snippets.Find(id);
            if (snippet == null)
            {
                return HttpNotFound();
            }
            return View(snippet);
        }

        // POST: Snippets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Link,description")] Snippet snippet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(snippet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(snippet);
        }

        // GET: Snippets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Snippet snippet = db.snippets.Find(id);
            if (snippet == null)
            {
                return HttpNotFound();
            }
            return View(snippet);
        }

        // POST: Snippets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Snippet snippet = db.snippets.Find(id);
            db.snippets.Remove(snippet);
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
