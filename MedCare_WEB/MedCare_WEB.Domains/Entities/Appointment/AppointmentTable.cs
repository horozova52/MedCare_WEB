using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.Domains.Entities.Appointment
{
     public class AppointmentTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Display(Name = "First Name")]
          public string FirstName { get; set; }

          [Display(Name = "Last Name")]
          public string LastName { get; set; }

          [Display(Name = "Doctor")]
          public string Doctor { get; set; }

          [Display(Name = "Phone")]
          public string Phone { get; set; }

          [Display(Name = "Date")]
          public DateTime Date { get; set; }

          [Display(Name = "Time")]
          public DateTime Time { get; set; }

          [Display(Name = "Message")]
          public string Message { get; set; }

          [Display(Name = "User Id")]
          public int UserId { get; set; }
     }
}
