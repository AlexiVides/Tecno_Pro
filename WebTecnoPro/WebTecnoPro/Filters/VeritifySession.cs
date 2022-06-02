using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTecnoPro.Controllers;
using WebTecnoPro.Models;
namespace WebTecnoPro.Filters
{
    public class VeritifySession : ActionFilterAttribute
    {
        private Usuario oUsuario;
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                base.OnActionExecuted(filterContext);
                oUsuario = (Usuario)HttpContext.Current.Session["User"];
                if (oUsuario == null)
                {
                    if (filterContext.Controller is LoginInicioController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/LoginInicio/Index");
                    }
                    else
                    {
                        //if (filterContext.Controller is LoginController == true)
                        //{
                        //   filterContext.HttpContext.Response.Redirect("/Vista/Index");
                        //}
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/LoginInicio/Index");
            }


            base.OnActionExecuted(filterContext);
        }
    }
}