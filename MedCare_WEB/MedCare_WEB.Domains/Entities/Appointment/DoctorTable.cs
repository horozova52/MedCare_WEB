using MedCare_WEB.Domains.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.Domains.Entities.Doctor
{
     public class DoctorTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Display(Name = "Username")]
          public string Username { get; set; }

          [Display(Name = "Email Address")]
          public string Email { get; set; }

          [Display(Name = "Phone")]
          public string Phone { get; set; }

          [Display(Name = "Type")]
          public string Type { get; set; }

          [Display(Name = "Description")]
          public string Description { get; set; }

          [Display(Name = "Image")]
          public string Image { get; set; }
     }
}
