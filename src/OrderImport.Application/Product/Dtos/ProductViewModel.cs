namespace OrderImport.Application.Product.Dtos
{
    public class ProductViewModel
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
