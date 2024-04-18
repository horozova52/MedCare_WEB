using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.Domains.Entities.User
{
    public class UResponseLogin
    {
        public bool Status { get; set; }

        public UResponseLogin(string statusMsg) => StatusMsg = statusMsg;

        public string StatusMsg { get; set; }
    }
}