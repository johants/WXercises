using System.Collections.Generic;

namespace WXercises.Models
{
    public class TrolleyTotalRequest
    {
        public List<BaseProduct> Products { get; set; }
        
        public List<ProductSpecial> Specials { get; set; }

        public List<ProductQuantity> Quantities { get; set; }
    }
}
