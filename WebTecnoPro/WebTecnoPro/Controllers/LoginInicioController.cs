using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTecnoPro.Models;

namespace WebTecnoPro.Controllers
{
    public class LoginInicioController : Controller
    {
        // GET: LoginInicio
        private TecnoProEntities db = new TecnoProEntities();
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(Usuario log)
        {


            //    var user = db.Usuario.Where(x => x.correo.Trim() == log.correo && x.contra == log.contra.Trim()).FirstOrDefault();

            //    if (user != null)
            //    {
            //       return Redirect("/Home/Index");

            //    }
            //    else
            //    {
            //        ViewData["Mensaje"] = "Incorrecto";
            //        return View();
            //    }
            //}





            try
            {
                using (TecnoProEntities db = new TecnoProEntities())
                {
                    var user = db.Usuario.Where(x => x.correo.Trim() == log.correo && x.contra == log.contra.Trim()).FirstOrDefault();

                    if (user == null)
                    {
                        ViewData["Mensaje"] = "Incorrecto";
                        return View();

                    }

                    Session["User"] = user;

                }
                return RedirectToAction("Index", "Home");

            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
    }



}
