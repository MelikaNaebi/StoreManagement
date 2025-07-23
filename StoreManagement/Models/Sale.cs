
namespace APIStoreManagement.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal Soldprice { get; set; }

        public DateTime Date { get; set; }
        public string? Description{ get; set; }
        public decimal? TotalSalesAmount { get; set; }

        public int ClothingId { get; set; }
        public Clothing Clothing { get; set; }

        public decimal? Discount { get; set; }

        public decimal InitialDeposit { get; set; } 
        public decimal AmountDue => Soldprice - (Discount ?? 0) - InitialDeposit; 

        public int? OriginatingOrderId { get; set; }

        // public int? Quantity { get; set; }
    }
}
