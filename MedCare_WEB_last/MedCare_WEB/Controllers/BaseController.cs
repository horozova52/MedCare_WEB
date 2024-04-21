﻿using MedCare_WEB.BusinessLogic;
using MedCare_WEB.BusinessLogic.Interfaces;
using MedCare_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedCare_WEB.Domains.Entities.User;
using MedCare_WEB.Domains.Enums;
using MedCare_WEB.Extension;

namespace MedCare_WEB.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ISession _session;

        public BaseController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
        }

        public bool userLoggedIn()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return false;
            }
            var apiCookie = Request.Cookies["X-KEY"];

            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);

                if (profile != null)
                {
                    return true;
                }
            }
            return false;
        }

        internal void SessionStatus()
        {
            var apiCookie = Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);
                if (profile != null)
                {
                    System.Web.HttpContext.Current.SetMySessionObject(profile);
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                }
                else
                {
                    System.Web.HttpContext.Current.Session.Clear();
                    if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
                    {
                        var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                        }
                    }

                    System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
            }
        }
    }
}