using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icecreamshop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Ange ditt förnamn")]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ange ditt efternamn")]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ange gatuadress")]
        [Display(Name = "Gatuadress")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Ange postnummer")]
        [Display(Name = "Postnummer")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Ange postort")]
        [Display(Name = "Postort")]
        public string Town { get; set; }

    }
}
