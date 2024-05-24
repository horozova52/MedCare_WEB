using MedCare_WEB.Attributes;
using MedCare_WEB.BusinessLogic.Interfaces;
using MedCare_WEB.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedCare_WEB.BusinessLogic.AppBL;
using MedCare_WEB.Domains.Entities.User;
using AutoMapper;
using MedCare_WEB.Domains.Entities.Admin;
using MedCare_WEB.Models;
using MedCare_WEB.Domains.Entities.Doctor;
using MedCare_WEB.Domains.Entities.Appointment;
using System.IO;
using System.Web.UI.WebControls;

namespace MedCare_WEB.Controllers
{
    public class AdminController : BaseController
    {
          private readonly ISession _session;
          private readonly IAdmin _admin;
          public AdminController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
               _admin = bl.GetAdminBL();
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

          public ActionResult AddUser()
          {
               GetStatus();
               using (var db = new TableContext())
               {
                    List<UserTable> usersList = db.Users.OrderByDescending(u => u.Level).ToList();
                    ViewBag.usersList = usersList;
               }
               return View();
          }

          public ActionResult AddDoctor()
          {
               GetStatus();
               List<string> types = new List<string> { "Neurology", "Opthalmology", "Nuclear Magnetic", "Surgical", "Cardiology", "Cardiology", "X-ray", "Dental", "Traumatology" };
               ViewBag.types = types;
               using (var db = new TableContext())
               {
                    List<DoctorTable> doctorList = db.Doctors.ToList();
                    ViewBag.doctorsList = doctorList;
               }
               return View();
          }

          public ActionResult ShowAppointment()
          {
               GetStatus();
               using (var db = new TableContext())
               {
                    List<AppointmentTable> AppointmentList = db.Appointments.ToList();
                    ViewBag.AppointmentsList = AppointmentList;
               }
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult AddUser(AddUser user)
          {
               if (ModelState.IsValid)
               {
                    var data = Mapper.Map<AddUserData>(user);

                    var addUser = _admin.AddUser(data);
                    if (addUser.Status)
                    {
                         return RedirectToAction("AddUser", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", addUser.StatusMsg);
                         return View();
                    }
               }
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult AddDoctor(AddDoctor doctor, HttpPostedFileBase imageFile)
          {
               if (ModelState.IsValid)
               {
                    if (imageFile != null && imageFile.ContentType == "image/png")
                    {
                         using (var db = new TableContext())
                         {
                              var path = Path.Combine(Server.MapPath($"~/Contents/images/doctors/{doctor.Username}.png"));
                              imageFile.SaveAs(path);
                         }
                    }

                    var data = Mapper.Map<AddDoctorData>(doctor);

                    var addDoctor = _admin.AddDoctor(data);

                    if (addDoctor.Status)
                    {
                         return RedirectToAction("AddDoctor", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", addDoctor.StatusMsg);
                         return View();
                    }
               }
               return View();
          }

          public ActionResult DeleteUser(int id)
          {
               _admin.DeleteUser(id);
               return RedirectToAction("AddUser", "Admin");
          }

          public ActionResult DeleteDoctor(int id)
          {
               using (var db = new TableContext())
               {
                    DoctorTable doctor = db.Doctors.FirstOrDefault(u => u.Id == id);
                    var path = Path.Combine(Server.MapPath($"~/Contents/images/doctors/{doctor.Image}"));
                    System.IO.File.Delete(path);
               }
               _admin.DeleteDoctor(id);
               return RedirectToAction("AddDoctor", "Admin");
          }

          public ActionResult DeleteAppointment(int id)
          {
               _admin.DeleteAppointment(id);
               return RedirectToAction("ShowAppointment", "Admin");
          }

          public ActionResult EditUser(int id)
          {
               GetStatus();
               using (var db = new TableContext())
               {
                    var user = db.Users.FirstOrDefault(u => u.Id == id);
                    var data = Mapper.Map<EditUser>(user);

                    ViewBag.userToEdit = data;
                    return View(data);
               }
          }

          public ActionResult EditDoctor(int id)
          {
               GetStatus();
               List<string> types = new List<string> { "Neurology", "Opthalmology", "Nuclear Magnetic", "Surgical", "Cardiology", "Cardiology", "X-ray", "Dental", "Traumatology" };
               ViewBag.types = types;
               using (var db = new TableContext())
               {
                    var doctor = db.Doctors.FirstOrDefault(u => u.Id == id);
                    var data = Mapper.Map<EditDoctor>(doctor);

                    ViewBag.doctorToEdit = data;
                    return View(data);
               }
          }

          public ActionResult EditAppointment(int id)
          {
               GetStatus();
               List<string> doctors = _session.GetDoctorList().Select(d => d.Username).ToList();
               ViewBag.doctors = doctors;
               using (var db = new TableContext())
               {
                    var appointment = db.Appointments.FirstOrDefault(u => u.Id == id);
                    var data = Mapper.Map<EditAppointment>(appointment);

                    ViewBag.appointmentToEdit = data;
                    return View(data);
               }
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult EditUser(EditUser user)
          {
               if (ModelState.IsValid)
               {
                    var data = Mapper.Map<EditUserData>(user);

                    var editUser = _admin.EditUser(data);
                    if (editUser.Status)
                    {
                         return RedirectToAction("AddUser", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", editUser.StatusMsg);
                         return View();
                    }
               }
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult EditDoctor(EditDoctor doctor, HttpPostedFileBase imageFile)
          {
               if (ModelState.IsValid)
               {
                    if (imageFile != null && imageFile.ContentType == "image/png")
                    {
                         using (var db = new TableContext())
                         {
                              DoctorTable existingDoctor = db.Doctors.FirstOrDefault(u => u.Email == doctor.Email);
                              var oldPath = Path.Combine(Server.MapPath($"~/Contents/images/doctors/{existingDoctor.Username}.png"));
                              var newPath = Path.Combine(Server.MapPath($"~/Contents/images/doctors/{doctor.Username}.png"));
                              existingDoctor.Image = doctor.Username + ".png";
                              db.SaveChanges();
                              System.IO.File.Delete(oldPath);
                              imageFile.SaveAs(newPath);
                         }
                    }

                    var data = Mapper.Map<EditDoctorData>(doctor);

                    var editDoctor = _admin.EditDoctor(data);
                    if (editDoctor.Status)
                    {
                         return RedirectToAction("AddDoctor", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", editDoctor.StatusMsg);
                         return View();
                    }
               }
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult EditAppointment(EditAppointment appointment)
          {
               if (ModelState.IsValid)
               {
                    var data = Mapper.Map<EditAppointmentData>(appointment);

                    var editAppointment = _admin.EditAppointment(data);
                    if (editAppointment.Status)
                    {
                         return RedirectToAction("ShowAppointment", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", editAppointment.StatusMsg);
                         return View();
                    }
               }
               return View();
          }
     }
}