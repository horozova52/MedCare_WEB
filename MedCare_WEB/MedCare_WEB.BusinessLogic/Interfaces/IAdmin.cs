using MedCare_WEB.Domains.Entities.Admin;
using MedCare_WEB.Domains.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.BusinessLogic.Interfaces
{
     public interface IAdmin
     {
          BoolResp AddUser(AddUserData data);
          BoolResp AddDoctor(AddDoctorData data);
          BoolResp EditUser(EditUserData data);
          BoolResp EditDoctor(EditDoctorData data);
          BoolResp EditAppointment(EditAppointmentData data);
          void DeleteUser(int id);
          void DeleteDoctor(int id);
          void DeleteAppointment(int id);
     }
}
