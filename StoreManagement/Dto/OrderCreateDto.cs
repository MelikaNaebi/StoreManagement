namespace APIStoreManagement.Dto
{// Dto/OrderCreateDto.cs
    public class OrderCreateDto
    {
        public decimal Deposit { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public int ClothingId { get; set; }
        public string? Description { get; set; }


        // OrderDate و IsDelivered را می‌توانید در بک‌اند مقداردهی کنید
    }
}
