using MedCare_WEB.Domains.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedCare_WEB.Models
{
     public class EditUser
     {
          public string Username { get; set; }

          public string Password { get; set; }

          public string Email { get; set; }

          public URole Level { get; set; }
     }
}