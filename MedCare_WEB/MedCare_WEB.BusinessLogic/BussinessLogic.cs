using MedCare_WEB.BusinessLogic.Interfaces;
using project_CAN.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.BusinessLogic
{
    public class BussinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}
