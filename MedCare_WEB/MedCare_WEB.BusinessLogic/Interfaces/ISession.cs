using MedCare_WEB.Domains.Entities.Appointment;
using MedCare_WEB.Domains.Entities.Doctor;
using MedCare_WEB.Domains.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MedCare_WEB.BusinessLogic.Interfaces
{
    public interface ISession
    {
        BoolResp UserLoginSessionBL(ULoginData data);
        BoolResp UserRegistrationSessionBL(URegisterData data);
        BoolResp AddAppointment(AddAppointmentData data);
        List<UserTable> GetUserList();
        List<DoctorTable> GetDoctorList();
        List<AppointmentTable> GetAppointmentList();
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
    }
}
