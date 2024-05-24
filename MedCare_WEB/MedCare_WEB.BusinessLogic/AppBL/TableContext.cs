using MedCare_WEB.Domains.Entities.Appointment;
using MedCare_WEB.Domains.Entities.Doctor;
using MedCare_WEB.Domains.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.BusinessLogic.AppBL
{
     public class TableContext : DbContext
     {
          public TableContext() : base("name=MEDCARE")
          {
          }

          public virtual DbSet<UserTable> Users { get; set; }
          public virtual DbSet<Session> Session { get; set; }
          public virtual DbSet<DoctorTable> Doctors { get; set; }
          public virtual DbSet<AppointmentTable> Appointments { get; set; }
     }
}
