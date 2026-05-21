using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Model;

namespace VGProducts.Repository.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        // DbSet for each entity
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Review> Review { get; set; }


        // Configure relationships 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //one to many relationship between User and Order
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Order)
                .WithOne(o => o.ApplicationUser)
                .HasForeignKey(o => o.UserId);

            // one to one relationship between User and CartItems
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<Cart>(c => c.UserId);

            //one to many relationship between Cart and CartItems
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            // one to many relationship between User and Favourites
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Favourites)
                .WithOne(f => f.ApplicationUser)
                .HasForeignKey(f => f.UserId);
            // one to many relationship between Category and SubCategory
            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategory)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId);
            // one to many relationship between SubCategory and Product
            modelBuilder.Entity<SubCategory>()
                .HasMany(s => s.Product)
                .WithOne(p => p.SubCategory)
                .HasForeignKey(p => p.SubCategoryId);
            //one to many relationship between Product and OrderItem
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItem)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId);
            // one to many relationship between Product and CartItems
            modelBuilder.Entity<Product>()
                .HasMany(p => p.CartItems)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);
            // one to many relationship between Order and OrderItem
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            // one to many relationship between Country and State
            modelBuilder.Entity<Country>()
                .HasMany(c => c.State)
                .WithOne(s => s.Country)
                .HasForeignKey(s => s.CountryId);
            //one to many relationship between State and City
            modelBuilder.Entity<State>()
                .HasMany(s => s.City)
                .WithOne(c => c.State)
                .HasForeignKey(c => c.StateId);
            // one to many relationship between City and Address
            modelBuilder.Entity<City>()
                .HasMany(c => c.Address)
                .WithOne(a => a.City)
                .HasForeignKey(a => a.CityId);

            // one to one relationship between State and Address
            modelBuilder.Entity<State>()
                .HasMany(s => s.Address)
                .WithOne(a => a.State)
                .HasForeignKey(a => a.StateId);

            // one to one relationship between Country and Address
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Address) 
                .WithOne(a => a.Country)
                .HasForeignKey(a => a.CountryId);

            //one to one relationship between City and Address
            modelBuilder.Entity<City>()
                .HasMany(c => c.Address)
                .WithOne(a => a.City)
                .HasForeignKey(a => a.CityId);

            //one to many relationship between User and Address
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Address)
                .WithOne(a => a.ApplicationUser)
                .HasForeignKey(a => a.UserId);

            // one to many relationship between Product and Review
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId);
            // one to many relationship between User and Review
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany() // one address can be used in many orders
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<int>();
            modelBuilder.Entity<Order>()
                .Property(o => o.PaymentMethod)
                .HasConversion<int>();
            modelBuilder.Entity<Order>()
                .Property(o => o.PaymentStatus)
                .HasConversion<int>();
            modelBuilder.Entity<Category>()
                .Property(c => c.IsActive)
                .HasConversion<int>();

        }
    }
}
