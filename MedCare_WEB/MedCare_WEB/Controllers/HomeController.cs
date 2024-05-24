using MedCare_WEB.BusinessLogic.Interfaces;
using MedCare_WEB.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MedCare_WEB.BusinessLogic.AppBL;
using MedCare_WEB.Models;
using MedCare_WEB.Domains.Entities.User;

namespace MedCare_WEB.Controllers
{
    public class HomeController : BaseController
    {
          private readonly ISession _session;
          public HomeController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }

          public void GetStatus()
          {
               SessionStatus();
               var apiCookie = System.Web.HttpContext.Current.Request.Cookies["X-KEY"];
               string userStatus = (string)System.Web.HttpContext.Current.Session["LoginStatus"];
               if (userStatus != "guest")
               {
                    var profile = _session.GetUserByCookie(apiCookie.Value);
                    ViewBag.level = profile.Level;
               }
               ViewBag.userStatus = userStatus;
          }

          public ActionResult Index()
          {
               GetStatus();
               var doctors = _session.GetDoctorList().Take(4);
               ViewBag.doctors = doctors;
               return View();
          }

          public ActionResult UserAppointments()
          {
               GetStatus();
               var apiCookie = System.Web.HttpContext.Current.Request.Cookies["X-KEY"];
               var profile = _session.GetUserByCookie(apiCookie.Value);
               var appointments = _session.GetAppointmentList().Where(a => a.UserId == profile.Id);
               ViewBag.appointments = appointments;
               return View();
          }

          public ActionResult Appointment()
          {
               GetStatus();
               List<string> doctors = _session.GetDoctorList().Select(d => d.Username).ToList();
               ViewBag.doctors = doctors;
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Appointment(AddAppointment appointment)
          {
               if (ModelState.IsValid)
               {
                    List<string> doctors = _session.GetDoctorList().Select(d => d.Username).ToList();
                    ViewBag.doctors = doctors;
                    var data = Mapper.Map<AddAppointmentData>(appointment);

                    var apiCookie = System.Web.HttpContext.Current.Request.Cookies["X-KEY"];
                    var profile = _session.GetUserByCookie(apiCookie.Value);

                    data.UserId = profile.Id;

                    var addAppointment = _session.AddAppointment(data);
                    if (addAppointment.Status)
                    {
                         return RedirectToAction("Appointment", "Home");
                    }
                    else
                    {
                         ModelState.AddModelError("", addAppointment.StatusMsg);
                         return View();
                    }
               }
               return View();
          }
     }
}