using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.Domains.Entities.User
{
    public class URegisterData
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime LoginDateTime { get; set; }

        public string LoginIp { get; set; }
    }
}
