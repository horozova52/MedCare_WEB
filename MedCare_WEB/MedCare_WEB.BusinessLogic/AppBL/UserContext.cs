using MedCare_WEB.Domains.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.BusinessLogic.AppBL
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("name=MEDCARE")
        {
        }

        public virtual DbSet<UserTable> Users { get; set; }
    }
}
