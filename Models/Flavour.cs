using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Required(ErrorMessage = "Ange ett pris")]
        [Display(Name = "Pris")]
        public double FlavourPrice { get; set; }

        public string PhotoPath { get; set; }

        public virtual ICollection<OrderBox> OrderBox { get; set; }//Many-to-many relationship connection
    }
}
