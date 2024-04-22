using MedCare_WEB.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedCare_WEB.Controllers
{
    public class AdminController : BaseController
    {
        [AdminMode]
        public ActionResult Doctors()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
    }
}