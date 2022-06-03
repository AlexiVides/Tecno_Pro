using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebTecnoPro.Models;

namespace WebTecnoPro.Controllers
{
    public class UsuariosController : Controller
    {
        private TecnoProEntities db = new TecnoProEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuario = db.Usuario.Where(v => v.estado == "Activo");
            return View(usuario.ToList());
            //return View(db.Usuario.ToList());
        }

        public ActionResult Inactivo()
        {
            var usuario = db.Usuario.Where(v => v.estado == "desactivado");
            return View(usuario.ToList());

        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuario,correo,contra,estado")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario.estado = "Activo";
                    db.Usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                
            }
            catch
            {
                ViewData["Mensaje"] = "Error";
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,correo,contra,estado")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
         
            return View(usuario);
        }

       


        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult desactivar(int id)
        {
            Usuario usuario = db.Usuario.Find(id);

            if (ModelState.IsValid)
            {
                if (usuario.estado == "Activo")
                {
                    usuario.estado = "desactivado";
                }
                else
                {
                    usuario.estado = "Activo";
                }
               
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
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
