using MedCare_WEB.BusinessLogic.Interfaces;

namespace MedCare_WEB.BusinessLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}
