using System.Drawing;

namespace APIStoreManagement.Models
{
    public class Orders
    {
        public int Id { get; set; }

        public decimal Deposit { get; set; }

        public String Fullname { get; set; }

        public DateTime OrderDate { get; set; }
        public string ? Description { get; set; }

        public int PatternId { get; set; }
        public int SizeId { get; set; }

        public Pattern Pattern { get; set; }
        public Sizes Sizes { get; set; }
    }
}

