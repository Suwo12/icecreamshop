using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icecreamshop.Models
{
    public class Flavour
    {
        public int FlavourId { get; set; }

        [Required(ErrorMessage = "Ange namn på smak")]
        [Display(Name = "Smak")]
        public string FlavourName { get; set; }

        [Required(ErrorMessage = "Ange en förteckning över smak")]
        [Display(Name = "Beskrivning")]
        public string FlavourDescription { get; set; }

        public virtual ICollection<Product> Products { get; set; }//Many-to-many relationship connection
    }
}
