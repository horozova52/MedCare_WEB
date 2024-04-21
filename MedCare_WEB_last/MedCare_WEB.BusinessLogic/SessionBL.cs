using MedCare_WEB.BusinessLogic.Core;
using MedCare_WEB.BusinessLogic.Interfaces;
using MedCare_WEB.Domains.Entities.User;
using System.Web;

namespace MedCare_WEB.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public UResponseLogin UserLoginSession(ULoginDataDomains dataDomains)
        {
            return UserLoginAction(dataDomains);
        }

        public HttpCookie GenCookie(string login)
        {
            return Cookie(login);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

        public UResponseLogin UserRegisterSession(URegisterDomains data)
        {
            return UserRegistrationAction(data);
        }
    }
}
