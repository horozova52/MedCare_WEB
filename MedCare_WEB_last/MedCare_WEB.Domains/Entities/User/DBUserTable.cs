using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedCare_WEB.Domains.Enums;

namespace MedCare_WEB.Domains.Entities.User
{
    [Table("users")]
    public class DBUserTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        [Required]
        [Display(Name = "username")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "username")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        public string password { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(256)]
        public string email { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public URole role { get; set; }

        [Required]
        public bool banStatus { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime lastLogin { get; set; }
    }
}
