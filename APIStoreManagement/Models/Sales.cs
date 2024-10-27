namespace APIStoreManagement.Models
{
    public class Sales
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public decimal? TotalSalesAmount { get; set; }

        public int ClothingId { get; set; }
        public Clothing Clothing { get; set; }

        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}
