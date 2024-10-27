namespace APIStoreManagement.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }

        public decimal Deposit { get; set; }

        public String Fullname { get; set; }

        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public int SizeId { get; set; }
      

        public int PatternId { get; set; }
    }
}
