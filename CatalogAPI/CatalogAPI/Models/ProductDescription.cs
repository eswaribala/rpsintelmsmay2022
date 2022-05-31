namespace CatalogAPI.Models
{
    public enum ProductType { REGULAR, SEASONAL}
    public class ProductDescription
    {
        public DateTime ManuFacturingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Detail { get; set; }
        public int UnitPrice { get; set; }

        public ProductType ProductType { get; set; }


    }
}
