using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Models
{
    public enum ProductType { REGULAR, SEASONAL}
    [Owned]
    public class ProductDescription
    {
         [DataType(DataType.Date)]
         [DisplayFormat(DataFormatString ="{0:MM dd yyyy}")]
        [Column("Manufacturing_Date")]
        public DateTime ManuFacturingDate { get; set; }
        [Column("Expiry_Date")]
        public DateTime ExpiryDate { get; set; }
        [Column("Detail")]
        public string Detail { get; set; }
        [Column("Unit_Price")]
        public int UnitPrice { get; set; }
        [Column("Product_Type")]
       
        public ProductType ProductType { get; set; }




    }
}
