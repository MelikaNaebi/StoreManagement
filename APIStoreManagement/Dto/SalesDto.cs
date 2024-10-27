namespace APIStoreManagement.Dto
{
    public class SalesDto
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalSalesAmount { get; set; }
        public int InventoryID { get; set; }
        public int ClothingId { get; set; }
    }
}
