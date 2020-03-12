using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icecreamshop.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Range(Int32.MinValue, 20)]
        public int Amount { get; set; }

        public int OrderBoxId { get; set; }

        public virtual ICollection<OrderBox> OrderBox { get; set; }//1-n relationship with orderbox, one order consists many products
        public virtual Flavour Flavour { get; set; }//1-n relationship with orderbox, one product consists many flavorus
    }
}
