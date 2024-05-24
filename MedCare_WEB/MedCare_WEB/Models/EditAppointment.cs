using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedCare_WEB.Models
{
     public class EditAppointment
     {
          public string FirstName { get; set; }

          public string LastName { get; set; }

          public string Doctor { get; set; }

          public string Phone { get; set; }

          public DateTime Date { get; set; }

          public DateTime Time { get; set; }

          public string Message { get; set; }

          public int UserId { get; set; }
     }
}