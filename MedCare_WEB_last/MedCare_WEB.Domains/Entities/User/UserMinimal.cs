using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedCare_WEB.Domains.Enums;

namespace MedCare_WEB.Domains.Entities.User
{
    public class UserMinimal
    {
        public int userId { get; set; }
        public string email { get; set; }
        public DateTime lastLogin { get; set; }
        public URole role { get; set; }
    }
}
