using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedCare_WEB.Domains.Enums;
namespace MedCare_WEB.Domains.Entities.User
{
    public class ULoginDataDomains
    {
        public string password { get; set; }
        public string email { get; set; }

        public DateTime lastLogin { get; set; }
    }
}
