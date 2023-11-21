using System.ComponentModel.DataAnnotations;

namespace Challenge.Entity
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        public string? SupplierName { get; set; }

        public string? SupplierAdress { get; set; }
    }
}
