using MedCare_WEB.BusinessLogic.AppBL;
using MedCare_WEB.Domains.Entities.Admin;
using MedCare_WEB.Domains.Entities.Appointment;
using MedCare_WEB.Domains.Entities.Doctor;
using MedCare_WEB.Domains.Entities.User;
using MedCare_WEB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.BusinessLogic.Core
{
     public class AdminApi
     {
          internal BoolResp AddUserAction(AddUserData data)
          {
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    using (var db = new TableContext())
                    {
                         UserTable existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email);
                         if (existingUser != null)
                         {
                              return new BoolResp { Status = false, StatusMsg = "Email already registered." };
                         }

                         var newUser = new UserTable
                         {
                              Username = data.Username,
                              Password = LoginHelper.HashGen(data.Password),
                              Email = data.Email,
                              Level = data.Level,
                              LastLogin = DateTime.Now,
                              LastIp = "None",
                         };
                         db.Users.Add(newUser);
                         db.SaveChanges();
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }

          internal BoolResp AddDoctorAction(AddDoctorData data)
          {
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    using (var db = new TableContext())
                    {
                         DoctorTable existingDoctor = db.Doctors.FirstOrDefault(u => u.Email == data.Email);
                         if (existingDoctor != null)
                         {
                              return new BoolResp { Status = false, StatusMsg = "Email already registered." };
                         }

                         var newDoctor = new DoctorTable
                         {
                              Username = data.Username,
                              Email = data.Email,
                              Phone = data.Phone,
                              Type = data.Type,
                              Description = data.Description,
                              Image = data.Username + ".png",
                         };
                         db.Doctors.Add(newDoctor);
                         db.SaveChanges();
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }

          internal void DeleteUserAction(int id)
          {
               using (var db = new TableContext())
               {
                    UserTable user = db.Users.FirstOrDefault(u => u.Id == id);
                    db.Users.Remove(user);
                    db.SaveChanges();
               }
          }

          internal void DeleteDoctorAction(int id)
          {
               using (var db = new TableContext())
               {
                    DoctorTable doctor = db.Doctors.FirstOrDefault(u => u.Id == id);
                    db.Doctors.Remove(doctor);
                    db.SaveChanges();
               }
          }

          internal void DeleteAppointmentAction(int id)
          {
               using (var db = new TableContext())
               {
                    AppointmentTable appointment = db.Appointments.FirstOrDefault(u => u.Id == id);
                    db.Appointments.Remove(appointment);
                    db.SaveChanges();
               }
          }

          internal BoolResp EditUserAction(EditUserData data)
          {
               UserTable existingUser;
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    using (var db = new TableContext())
                    {
                         existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email);
                         existingUser.Username = data.Username;
                         existingUser.Email = data.Email;
                         existingUser.Level = data.Level;
                         db.SaveChanges();
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }

          internal BoolResp EditDoctorAction(EditDoctorData data)
          {
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    using (var db = new TableContext())
                    {
                         DoctorTable existingDoctor = db.Doctors.FirstOrDefault(u => u.Email == data.Email);
                         existingDoctor.Username = data.Username;
                         existingDoctor.Email = data.Email;
                         existingDoctor.Phone = data.Phone;
                         existingDoctor.Type = data.Type;
                         existingDoctor.Description = data.Description;
                         db.SaveChanges();
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }

          internal BoolResp EditAppointmentAction(EditAppointmentData data)
          {
               using (var db = new TableContext())
               {
                    AppointmentTable existingAppointment = db.Appointments.FirstOrDefault(u => u.Doctor == data.Doctor && u.Phone == data.Phone);

                    existingAppointment.FirstName = data.FirstName;
                    existingAppointment.LastName = data.LastName;
                    existingAppointment.Doctor = data.Doctor;
                    existingAppointment.Phone = data.Phone;
                    existingAppointment.Date = data.Date;
                    existingAppointment.Time = data.Time;
                    existingAppointment.Message = data.Message;
                    db.SaveChanges();
               }
               return new BoolResp { Status = true };
          }
     }
}
