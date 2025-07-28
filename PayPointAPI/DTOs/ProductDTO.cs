namespace PayPointAPI.DTOs
{
    public class ProductDTO
    {
        public string ProductName { get; set; } = default!;
        public string ProductDescription { get; set; } = default!;
        public int Price { get; set; }
        public int StockQuantity { get; set; }
        public DateOnly ExpiryDate { get; set; }

        public int CategoryId { get; set; }
    }
}
