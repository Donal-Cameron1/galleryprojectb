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
    public class GalleriesController : Controller
    {
        private galleryprojectbcontext db = new galleryprojectbcontext();

        // GET: Galleries
        public ActionResult Index(string search, int? pages)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return View(db.Gallery.Where(c => c.GalleryName.Contains(search) || search == null).OrderBy(X => X.GalleryName).ToPagedList(pages ?? 1, 5));
            }
                return View(db.Gallery.OrderBy(X => X.GalleryName).ToPagedList(pages ?? 1,5));
        }

        // GET: Galleries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Galleries galleries = db.Gallery.Find(id);
            if (galleries == null)
            {
                return HttpNotFound();
            }
            return View(galleries);
        }

        // GET: Galleries/Create
        public ActionResult Create()
        {
            
            if (this.User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return View("Notsignedin");
            }
            
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GalleryID,GalleryName,GalleryCreator")] Galleries galleries)
        {
            if (ModelState.IsValid)
            {
                galleries.GalleryCreator = User.Identity.Name;
                db.Gallery.Add(galleries);
                db.SaveChanges();
                return RedirectToAction("Mygalleries");
            }

            return View(galleries);
        }

        // GET: Galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Galleries galleries = db.Gallery.Find(id);
            if (galleries == null)
            {
                return HttpNotFound();
            }
            return View(galleries);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GalleryID,GalleryName,GalleryCreator")] Galleries galleries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(galleries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(galleries);
        }

        // GET: Galleries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Galleries galleries = db.Gallery.Find(id);
            if (galleries == null)
            {
                return HttpNotFound();
            }
            return View(galleries);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Galleries galleries = db.Gallery.Find(id);
            db.photo.RemoveRange(db.photo.Where(x => x.GalleryID == galleries.GalleryID));

            db.Gallery.Remove(galleries);
            db.SaveChanges();
            return RedirectToAction("Mygalleries");
        }
        public ActionResult Mygalleries(int? id, string search, int? pages)
        {
            
            if (this.User.Identity.IsAuthenticated)
            {
                string d = User.Identity.Name;
                if (!string.IsNullOrEmpty(search))
                {
                    return View(db.Gallery.Where(x => x.GalleryCreator == d && x.GalleryName.Contains(search) || search == null).OrderBy(X => X.GalleryName).ToPagedList(pages ?? 1, 5));
                }
                return View(db.Gallery.Where(s => s.GalleryCreator == d).OrderBy(x => x.GalleryName).ToPagedList(pages ?? 1, 5));
            }
            return View ("Notsignedin");
        }
        public ActionResult galleryphotos(int?id, string search, int? pages)
        {
            Galleries gallery = db.Gallery.Find(id);
            int name = gallery.GalleryID;
            if (!string.IsNullOrEmpty(search))
            {
                return View(db.photo.Where(x => x.GalleryID == name && x.ImageName.Contains(search) || search == null).OrderBy(X => X.ImageName).ToPagedList(pages ?? 1, 1));
            }
            return View(db.photo.Where(x => x.GalleryID == name).OrderBy(x => x.ImageName).ToPagedList(pages ?? 1, 1));
        }
        public ActionResult galleryphotos2(int? id, string search, int? pages)
        {
            Galleries gallery = db.Gallery.Find(id);
            int name = gallery.GalleryID;
            if (!string.IsNullOrEmpty(search))
            {
                return View(db.photo.Where(x => x.GalleryID == name && x.ImageName.Contains(search) || search == null).OrderBy(X=>X.ImageName).ToPagedList(pages ?? 1,1));
            }
                return View(db.photo.Where(x => x.GalleryID == name).OrderBy(x => x.ImageName).ToPagedList(pages ?? 1,1));
        }
        public ActionResult FullImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo galleries = db.photo.Find(id);
            if (galleries == null)
            {
                return HttpNotFound();
            }
            return View(galleries);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
        public ActionResult Edit2([Bind(Include = "ID,Image,ImageName,GalleryName")] photo photo, HttpPostedFileBase files, int? id)
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
            return RedirectToAction("Mygalleries", "Galleries");
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
