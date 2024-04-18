using MedCare_WEB.BusinessLogic.Interfaces;
using MedCare_WEB.Domains.Entities.User;
using project_CAN.BusinessLogic.Core;
using System.Web;

namespace project_CAN.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public UResponseLogin UserLoginSessionBL(ULoginData data)
        {
            return UserLoginAction(data);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

        public UResponseLogin UserRegistrationSessionBL(URegistrationData data)
        {
            return UserRegistrationAction(data);
        }
    }
}