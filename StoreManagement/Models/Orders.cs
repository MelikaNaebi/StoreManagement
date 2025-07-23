
namespace APIStoreManagement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public decimal Deposit { get; set; }

        public String CustomerName { get; set; }

        public string CustomerPhoneNumber { get; set; }
        public bool IsDelivered { get; set; }

        public int ClothingId { get; set; }
        public Clothing Clothing { get; set; }


        public DateTime OrderDate { get; set; }
        public string ? Description { get; set; }

        //    public int? Quantity { get; set; }


    }
}

