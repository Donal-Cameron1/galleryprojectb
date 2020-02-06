using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using galleryprojectb.DAL;
using galleryprojectb.Models;
using PagedList;

namespace galleryprojectb.Controllers
{
    public class photoController : Controller
    {
        private galleryprojectbcontext db = new galleryprojectbcontext();

        // GET: photo
        public ActionResult Index(string search, int? pages)
        {
            var b = db.User.Where(x => x.Username == User.Identity.Name).Select(x => x.role).SingleOrDefault();
            if(b == "owner")
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return View(db.photo.Where(c => c.ImageName.Contains(search) || search == null).OrderBy(X => X.ImageName).ToPagedList(pages ?? 1, 5));
                }

                return View(db.photo.OrderBy(x=> x.ImageName).ToPagedList(pages ?? 1, 5));
            }
            return View ("Notanowner");
        }

        // GET: photo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = db.photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: photo/Create
        public ActionResult Create(int? id)
        {
            Galleries gallery = db.Gallery.Find(id);
            ViewBag.value = Convert.ToInt32(gallery.GalleryID);
            return View();
        }

        // POST: photo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Image,ImageName,GalleryID")] photo photo, HttpPostedFileBase files, int?id)
        {
            
                Galleries gallery = db.Gallery.Find(id);
                ViewBag.value = Convert.ToInt32(gallery.GalleryID);

                // extract only the filename
                var fileName = Path.GetFileName(files.FileName);
                
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/Content"), fileName);
                files.SaveAs(path);

                photo.GalleryID = ViewBag.value;
                photo.Image = "~/Content/" + fileName;
                

                db.photo.Add(photo);
                db.SaveChanges();

            // redirect back to the index action to show the form once again

            return View("View");


        }

        // GET: photo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = db.photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: photo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Image,ImageName,GalleryID")] photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("View");
        }

        // GET: photo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = db.photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: photo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            photo photo = db.photo.Find(id);
            db.photo.Remove(photo);
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
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Edit2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = db.photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: photo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "ID,Image,ImageName,GalleryID")] photo photo, HttpPostedFileBase files, int? id)
        {
            if (ModelState.IsValid)
            { 

                // extract only the filename
                var fileName = Path.GetFileName(files.FileName);

                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/Content"), fileName);
                files.SaveAs(path);

               
                photo.Image = "~/Content/" + fileName;
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return View("Success");
            }
            return View("View");
        }
        public ActionResult Delete2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = db.photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: photo/Delete/5
        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {
            photo photo = db.photo.Find(id);
            db.photo.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Mygalleries","Galleries");
        }
        public ActionResult Details2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = db.photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }
    }
}
