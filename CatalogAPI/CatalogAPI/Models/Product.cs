using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Product_Id")]
        public long ProductId { get; set; }
        public ProductDescription Description { get; set; }

        [ForeignKey("Catalog")]
        [Column("Catalog_Id_FK")]
        private long CatalogId { get; set; }
        private Catalog Catalog { get; set; }
    }
}
