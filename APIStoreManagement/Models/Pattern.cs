namespace APIStoreManagement.Models
{
    public class Pattern
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Clothing> Clothings { get; set; }

        public ICollection<Orders> Orders { get; set; }

        public ICollection<Inventory> Inventories { get; set; }
    }
}

