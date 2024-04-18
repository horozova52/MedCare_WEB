using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedCare_WEB.Domains.Entities.User;

namespace MedCare_WEB.BusinessLogic.DBModel
{
    public class DBUserContext : DbContext
    {
        public DBUserContext() : base("name=MedCare_WEB_DB")
        {

        }

        public virtual DbSet<DBUserTable> Users { get; set; }
    }
}
