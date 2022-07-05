using System;
using System.ComponentModel.DataAnnotations;

namespace fooddotcomapi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [Required]
        [StringLength(60)]
        public string Email { get; set; }

        [Required]
        [StringLength(40)]
        public string Phone { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        public string Role { get; set; }
        //public ICollection<Reservation> Reservations { get; set; }
    }
}

