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
using System.IO;
using System.Data.Entity.Validation;
using System.Diagnostics;

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

       
       

        //public void LoadImage(string pageImage)
        //{
           
        //    byte[] bytes = Convert.FromBase64String(pageImage);

        //    Image image;
        //    using (MemoryStream ms = new MemoryStream(bytes))
        //    {
        //        image = Image.FromStream(ms);
        //    }

        //    image.Save(, ImageFormat.Jpeg);
        //}


        //public ActionResult extensionSavedefault(ExtensionInfo info)
        //{
        //    Snippet snippet = new Snippet
        //    {
        //        description = "test",


        //    }
        //}
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
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
        public ActionResult extensionView(FormCollection data)
        {
            TempData["imageData"] = data["imageData"]; 
            TempData["UrlData"] = data["url"];

            return RedirectToAction("ChromeCreate");
        }

        public ActionResult ChromeCreate(ExtensionInfo info)
        {

            /*info = (ExtensionInfo)TempData["modeltopass"]*/;
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
                {
                    Link = TempData["UrlData"].ToString(),
                    image = TempData["imageData"].ToString()
                }
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
              


                CurrentSnippetCollection.snippets.Add(snippet);
                

                db.snippets.Add(snippet);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
       
                db.SaveChanges();
                return RedirectToAction("Index");


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
