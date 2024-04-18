using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCare_WEB.Domains.Entities.User
{
    [Table("sessions")]
    public class DBSessionTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sessionId { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }

        public virtual DBUserTable User { get; set; }

        [Required]
        public string cookieValue { get; set; }

        [Required]
        public DateTime expireTime { get; set; }

    }
}
