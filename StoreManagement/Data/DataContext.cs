using Microsoft.EntityFrameworkCore;

namespace APIStoreManagement.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Clothing> Clothings { get; set; }
        public DbSet<Costs> Costs { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<Sale>Sales { get; set; }
        public DbSet<Pattern> Patterns { get; set; }
        public DbSet<Sizes> Sizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          

            modelBuilder.Entity<Pattern>()
                .HasMany(s => s.Clothings)
                .WithOne(c => c.Pattern)
                .HasForeignKey(c => c.PatternId)
                .OnDelete(DeleteBehavior.Restrict);

            //// ارتباط یک به چند بین  و Orders
            //modelBuilder.Entity<Pattern>()
            //    .HasMany(s => s.Orders)
            //    .WithOne(o => o.Pattern)
            //    .HasForeignKey(o => o.PatternId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// ارتباط یک به چند بین Sizes و Clothing
            //modelBuilder.Entity<Sizes>()
            //    .HasMany(sz => sz.Clothings)
            //    .WithOne(c => c.Sizes)
            //    .HasForeignKey(c => c.SizeId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// ارتباط یک به چند بین Sizes و Orders
            //modelBuilder.Entity<Sizes>()
            //    .HasMany(sz => sz.Orders)
            //    .WithOne(o => o.Sizes)
            //    .HasForeignKey(o => o.SizeId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
