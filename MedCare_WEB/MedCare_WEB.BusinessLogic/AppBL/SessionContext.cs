using MedCare_WEB.Domains.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.BusinessLogic.AppBL
{
    public class SessionContext : DbContext
    {
        public SessionContext() :
            base("name=MEDCARE")
        {
        }

        public virtual DbSet<Session> Session { get; set; }
    }
}
