using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPointAPI.Models.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string ProductDescription { get; set; } = default!;
        public int Price { get; set; }
        public int StockQuantity { get; set; }
        public DateOnly ExpiryDate { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int StoreId { get; set; }   
        public Store Store { get; set; }
    }
}
