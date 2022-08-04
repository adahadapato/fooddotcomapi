using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fooddotcomapi.Authorization
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(45)]
        public string FirstName { get; set; }


        [Required]
        [StringLength(45)]
        public string LastName { get; set; }
        public DateTime? DateCreated { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string Phone { get; set; }
        
        public string PictureUrl { get; set; }


        [NotMapped]
        public string RoleId { get; set; }
        [NotMapped]
        public string Role { get; set; }
        [NotMapped]
        public IEnumerable<string> Roles { get; set; }

    }
}
