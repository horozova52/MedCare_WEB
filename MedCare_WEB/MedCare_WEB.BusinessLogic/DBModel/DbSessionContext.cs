using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using MedCare_WEB.Domains.Entities.User;

namespace MedCare_WEB.BusinessLogic.DBModel
{
    public class DBSessionContext : DbContext
    {
        public DBSessionContext() : base("name=MedCare_WEB_DB")
        {

        }

        public virtual DbSet<DBSessionTable> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBSessionTable>()
                .HasRequired(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.userId); // Configure foreign key

            base.OnModelCreating(modelBuilder);
        }

    }
}
