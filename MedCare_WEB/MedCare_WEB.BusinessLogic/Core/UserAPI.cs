using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MedCare_WEB.Domains.Entities.User;
using MedCare_WEB.BusinessLogic.AppBL;
using MedCare_WEB.Domains.Entities.Enums;
using MedCare_WEB.Helpers;
using MedCare_WEB.Domains.Entities.Admin;
using MedCare_WEB.Domains.Entities.Appointment;
using MedCare_WEB.Domains.Entities.Doctor;

namespace project_CAN.BusinessLogic.Core
{
    public class UserApi
    {
        internal BoolResp UserLoginAction(ULoginData data)
        {
            UserTable result;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Email))
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new TableContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Email == data.Email && u.Password == pass);
                }

                if (result == null)
                {
                    return new BoolResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new TableContext())
                {
                    result.LastIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }
                return new BoolResp { Status = true };
            }
            else
                return new BoolResp { Status = false };
        }

        internal BoolResp UserRegisterAction(URegisterData data)
        {
            UserTable existingUser;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Email))
            {
                using (var db = new TableContext())
                {
                    existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email);
                }

                if (existingUser != null)
                {
                    return new BoolResp { Status = false, StatusMsg = "User With Email Already Exists" };
                }

                var pass = LoginHelper.HashGen(data.Password);
                var newUser = new UserTable
                {
                    Email = data.Email,
                    Username = data.Username,
                    Password = pass,
                    LastIp = data.LoginIp,
                    LastLogin = data.LoginDateTime,
                    Level = (URole)0,
                };

                using (var todo = new TableContext())
                {
                    todo.Users.Add(newUser);
                    todo.SaveChanges();
                }
                return new BoolResp { Status = true };
            }
            else
                return new BoolResp { Status = false };
        }

          internal BoolResp AddAppointmentAction(AddAppointmentData data)
          {
               using (var db = new TableContext())
               {
                    AppointmentTable appointment = db.Appointments.FirstOrDefault(u => u.Doctor == data.Doctor && u.Date == data.Date && u.Time.Hour == data.Time.Hour);
                    if (appointment != null)
                    {
                         return new BoolResp { Status = false, StatusMsg = "The doctor is busy at this time." };
                    }

                    var newAppointment = new AppointmentTable
                    {
                         FirstName = data.FirstName,
                         LastName = data.LastName,
                         Doctor = data.Doctor,
                         Phone = data.Phone,
                         Date = data.Date,
                         Time = data.Time,
                         Message = data.Message,
                         UserId = data.UserId
                    };
                    db.Appointments.Add(newAppointment);
                    db.SaveChanges();
               }
               return new BoolResp { Status = true };
          }

          internal List<UserTable> GetUserListAction()
          {
               List<UserTable> users;
               using (var db = new TableContext())
               {
                    users = db.Users.ToList();
               }
               return users;
          }

          internal List<DoctorTable> GetDoctorListAction()
          {
               List<DoctorTable> doctors;
               using (var db = new TableContext())
               {
                    doctors = db.Doctors.ToList();
               }
               return doctors;
          }

          internal List<AppointmentTable> GetAppointmentListAction()
          {
               List<AppointmentTable> appointments;
               using (var db = new TableContext())
               {
                    appointments = db.Appointments.ToList();
               }
               return appointments;
          }

          internal HttpCookie Cookie(string loginCredential)
        {
            int sessionTime = 60;
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new TableContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Session where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Session where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(sessionTime);
                    using (var todo = new TableContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Session.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(sessionTime)
                    });
                    db.SaveChanges();
                }
            }
            return apiCookie;
        }

        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UserTable curentUser;

            using (var db = new TableContext())
            {
                session = db.Session.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }
            if (session == null) return null;
            using (var db = new TableContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }
    }
}