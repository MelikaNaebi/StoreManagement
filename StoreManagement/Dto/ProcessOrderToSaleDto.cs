namespace APIStoreManagement.Dto
{
    public class ProcessOrderToSaleDto
    {
        public int OrderId { get; set; }
        public decimal SoldPrice { get; set; }
        public decimal? Discount { get; set; }
        public string? SaleDescription { get; set; }
    }
}
