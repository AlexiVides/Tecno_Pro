using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebTecnoPro.Models;

namespace WebTecnoPro.Controllers
{
    public class ProductosController : Controller
    {
        private TecnoProEntities db = new TecnoProEntities();

        // GET: Productos
        public ActionResult Index()
        {
            var prodcuto = db.Producto.Where(v => v.estado == "Activo");
            return View(prodcuto.ToList());

        }

        public ActionResult Inactivo()
        {
            var prodcuto = db.Producto.Where(v => v.estado == "Desactivo");
            return View(prodcuto.ToList());

        }




        public ActionResult convertirimagen(int id)
        {

            var imageload = db.Producto.Where(x => x.idProducto == id).FirstOrDefault();
            return File(imageload.imagen, "image/jpeg");
        }

        public ActionResult getImage(int id)
        {
            Producto ejemplok = db.Producto.Find(id);
            byte[] byteImage = ejemplok.imagen;

            MemoryStream memoryStream = new MemoryStream(byteImage);

            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }



        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProducto,nombre,cantidad,precio,detalle,marca,modelo,codigo,imagen,estado")] Producto producto, HttpPostedFileBase imagen)
        {

            HttpPostedFileBase fileBase = Request.Files[0];

            if (fileBase.ContentLength == 0)
            {
                ModelState.AddModelError("imagen", "Es necesario seleccionar una imagen.");
            }
            else
            {
                if (fileBase.FileName.EndsWith(".jpg"))
                {
                    WebImage image = new WebImage(fileBase.InputStream); ;

                    producto.imagen = image.GetBytes();
                }
                else
                {
                    ModelState.AddModelError("imagen", "Es necesario seleccionar una imagenes con formato JPG.");
                }

            }

            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProducto,nombre,cantidad,precio,detalle,marca,modelo,codigo,imagen,estado")] Producto producto)
        {

            byte[] imagenActual = null;
            HttpPostedFileBase fileBase = Request.Files[0];




            if (fileBase.ContentLength == 0)
            {
                imagenActual = db.Producto.SingleOrDefault(t => t.idProducto == producto.idProducto).imagen;
                producto.imagen = imagenActual;
            }
            else
            {
                if (fileBase.FileName.EndsWith(".jpg"))
                {
                    WebImage image = new WebImage(fileBase.InputStream); ;

                    producto.imagen = image.GetBytes();
                }
                else
                {
                    ModelState.AddModelError("imagen", "Es necesario seleccionar una imagenes con formato JPG.");
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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
