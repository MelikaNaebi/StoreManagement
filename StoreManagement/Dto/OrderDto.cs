namespace APIStoreManagement.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }

        public decimal Deposit { get; set; }
        public int ClothingId { get; set; }

        public String CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }

        public DateTime OrderDate { get; set; }
        public string? Description { get; set; }
        public string? PatternName { get; set; }
        public string? SizeName { get; set; }
        public bool IsDelivered { get; set; }
    }
}
