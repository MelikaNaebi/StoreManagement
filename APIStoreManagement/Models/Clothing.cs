namespace APIStoreManagement.Models
{
    public class Clothing
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int SizeId { get; set; }

        public Sizes Sizes { get; set; }

       public int PatternId { get; set; }

        public Pattern Pattern { get; set;}

        public ICollection<Inventory> Inventories { get; set; }
        public ICollection<Sales> Sales { get; set; }

    }
}
