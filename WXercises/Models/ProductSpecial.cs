using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WXercises.Models
{
    public class ProductSpecial
    {
        public List<ProductQuantity> Quantities { get; set; }
        public Decimal Total { get; set; }
    }
}
