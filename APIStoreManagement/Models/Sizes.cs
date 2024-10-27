namespace APIStoreManagement.Models
{
    public class Sizes
    {
        public int Id { get; set; }

        public string SizeName { get; set; }

        public ICollection<Clothing> Clothings { get; set; }
        public ICollection<Orders> Orders { get; set; }

        public ICollection<Inventory> Inventories { get; set; }
    }
}

