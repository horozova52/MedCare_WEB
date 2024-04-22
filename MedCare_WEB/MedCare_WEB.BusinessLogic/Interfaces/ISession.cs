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
        UResponseLogin UserLoginSessionBL(ULoginData data);
        UResponseRegister UserRegistrationSessionBL(URegisterData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
    }
}
