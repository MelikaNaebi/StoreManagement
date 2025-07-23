namespace APIStoreManagement.Dto
{
    public class SalesDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }

        public string? PatternName { get; set; } 
        public string? SizeName { get; set; }

        public int ClothingId { get; set; }


        public decimal SoldPrice { get; set; }

        public string Description{ get; set; }

        public DateTime Date { get; set; }

        public decimal TotalSalesAmount { get; set; }

        public int? Discount { get; set; }
        public decimal InitialDeposit { get; set; }  
        public decimal AmountDue => SoldPrice - (Discount ?? 0) - InitialDeposit;

        // public int? Quantity { get; set; }

    }
}
