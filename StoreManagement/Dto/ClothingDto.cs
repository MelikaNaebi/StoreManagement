namespace APIStoreManagement.DTOs
{
    public class ClothingDto
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int SizeId { get; set; }

        public int PatternId { get; set; }

        public string? PatternName { get; set; } // این خط را اضافه یا بررسی کنید
        public string? SizeName { get; set; }

        public List<int> InventoryIds { get; set; } // Collection of related inventory IDs

        public List<int> SaleIds { get; set; } // Collection of related sale IDs
    }
}
