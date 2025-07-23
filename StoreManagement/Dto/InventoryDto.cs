namespace APIStoreManagement.Dto
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int ClothingId { get; set; }

        public string PatternName { get; set; }
        public string SizeName { get; set; }
    }
}
