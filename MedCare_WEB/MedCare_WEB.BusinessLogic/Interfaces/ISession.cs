using System.Web;
using MedCare_WEB.Domains.Entities.User;
using MedCare_WEB.Domains.Entities.User;

namespace MedCare_WEB.BusinessLogic.Interfaces
{
    public interface ISession
    {
        UResponseLogin UserLoginSession(ULoginDataDomains dataDomains);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);

        UResponseLogin UserRegisterSession(URegisterDomains data);
    }
}
