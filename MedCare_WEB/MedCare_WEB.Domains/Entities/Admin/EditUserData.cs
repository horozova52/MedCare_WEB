using MedCare_WEB.Domains.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.Domains.Entities.Admin
{
     public class EditUserData
     {
          public string Username { get; set; }

          public string Password { get; set; }

          public string Email { get; set; }

          public URole Level { get; set; }
     }
}
