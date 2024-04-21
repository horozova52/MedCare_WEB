using AutoMapper;
using MedCare_WEB.BusinessLogic.Interfaces;
using MedCare_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MedCare_WEB.Domains.Entities.User;

namespace MedCare_WEB.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginView login)
        {
            if (ModelState.IsValid)
            {
                Mapper.Reset();
                Mapper.Initialize(cfg => cfg.CreateMap<UserLoginView, ULoginDataDomains>());
                var data = Mapper.Map<ULoginDataDomains>(login);

                data.lastLogin = DateTime.Now;

                var userLogin = _session.UserLoginSession(data);
                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ViewBag.Error = userLogin.StatusMsg;
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserRegisterView registerData)
        {
            if (ModelState.IsValid)
            {
                Mapper.Reset();
                Mapper.Initialize(cfg => cfg.CreateMap<URegisterDomains, UserRegisterView>());
                var data = Mapper.Map<URegisterDomains>(registerData);

                //data.lastLogin = DateTime.Now;
                var userRegister = _session.UserRegisterSession(data);
                if (userRegister.Status)
                {
                    HttpCookie cookie = _session.GenCookie(registerData.email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ViewBag.Error = userRegister.StatusMsg;
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
            {
                var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
            }

            System.Web.HttpContext.Current.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}