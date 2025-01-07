using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TGU.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string Permission { get; set; } = string.Empty;
    }

}
