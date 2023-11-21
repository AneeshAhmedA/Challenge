using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Entity
{
    public class Item
    {
        public int ItemID { get; set; }

        public string? ItemDescription{ get; set; }

        public double ItemRate{ get; set; }

        public int SupplierID { get; set; }

        [ForeignKey(nameof(SupplierID))]
        public Supplier Supplier { get; set; }
    }
}
