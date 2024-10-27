using System;

namespace APIStoreManagement.Models
{
    public class Costs
    {
        public int Id { get; set; }
        public string Title { get; set; }
  
        public decimal Amount { get; set; } 
        public DateTime Date { get; set; } 

        public string? Description { get; set; }

        public decimal? DailySalesAmount { get; set; }

    }
}
