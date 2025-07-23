namespace APIStoreManagement.Models
{
    public class Clothing
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int SizeId { get; set; }

        public Sizes Size { get; set; }

       public int PatternId { get; set; }

        public Pattern Pattern { get; set;}


        public string SizeName => Size?.SizeName; // Use Size?.SizeName or Size?.Name depending on your Sizes model property
        public string PatternName => Pattern?.Name;

        public ICollection<Inventory> Inventories { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
