using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icecreamshop.Models
{
    public class OrderBox
    {
        public int OrderBoxId { get; set; }
        [Display(Name = "Totalsumma")]
        public float OrderSum { get; set; }
        [DataType(DataType.Date)]//Datumstämpel för när köp sker
        public DateTime OrderDate { get; set; }//Datumstämpel för när köp sker
        [Display(Name = "Beställda produkter")]
        public int ProductId { get; set; }//Hämtar in ProductId från Product som FK
        [Display(Name = "Beställare")]
        public ApplicationUser User { get; set; }//Hämtar in userId från AspNetUser som FK

        public virtual Product Products { get; set; }//Many-to-many relationship connection
    }
}
