using Microsoft.AspNetCore.Identity;
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
        [Display(Name = "Beställda smaker")]
        public int FlavourId { get; set; }//Hämtar in ProductId från Product som FK
        [Display(Name = "Beställare")]
        public string UserId { get; set; }//Hämtar in userId från AspNetUser som FK
        public virtual ApplicationUser User { get; set; }//Hämtar in userId från AspNetUser som FK
        public virtual Flavour Flavour { get; set; }//Many-to-many relationship connection
    }
}