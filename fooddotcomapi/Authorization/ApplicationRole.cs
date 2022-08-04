using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace fooddotcomapi.Authorization
{
    public class ApplicationRole : IdentityRole
    {
        [Required]
        [StringLength(60)]
        public string Description { get; set; }
    }
}
