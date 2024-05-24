using MedCare_WEB.BusinessLogic.Core;
using MedCare_WEB.BusinessLogic.Interfaces;
using MedCare_WEB.Domains.Entities.Admin;
using MedCare_WEB.Domains.Entities.User;
using project_CAN.BusinessLogic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.BusinessLogic.AppBL
{
     public class AdminBL : AdminApi, IAdmin
     {
          public BoolResp AddUser(AddUserData data)
          {
               return AddUserAction(data);
          }

          public BoolResp AddDoctor(AddDoctorData data)
          {
               return AddDoctorAction(data);
          }

          public BoolResp EditUser(EditUserData data)
          {
               return EditUserAction(data);
          }

          public BoolResp EditDoctor(EditDoctorData data)
          {
               return EditDoctorAction(data);
          }

          public BoolResp EditAppointment(EditAppointmentData data)
          {
               return EditAppointmentAction(data);
          }

          public void DeleteUser(int id)
          {
               DeleteUserAction(id);
          }

          public void DeleteDoctor(int id)
          {
               DeleteDoctorAction(id);
          }

          public void DeleteAppointment(int id)
          {
               DeleteAppointmentAction(id);
          }
     }
}
