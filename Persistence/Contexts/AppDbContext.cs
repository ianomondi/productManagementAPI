using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using ProductManagement.API.Domain.Models;

namespace ProductManagement.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<VariantOption> VariantOptions { get; set; }
        public DbSet<SKU> SKUs { get; set; }
        public DbSet<CompositeProduct> CompositeProducts { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();//.HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
            builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Category>().HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

            builder.Entity<Category>().HasData
            (
                new Category { Id = 100, Name = "Fruits and Vegetables" }, // Id set manually due to in-memory provider
                new Category { Id = 101, Name = "Dairy" }
            );

            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().HasMany(p => p.CompositeProducts).WithOne().HasForeignKey(cp => cp.ProductId);
            builder.Entity<Product>().HasMany(p => p.CompositeProducts).WithOne().HasForeignKey(cp => cp.RelatedId);
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            //builder.Entity<Product>().Property(p => p.QuantityInPackage).IsRequired();
            //builder.Entity<Product>().Property(p => p.UnitOfMeasurement).IsRequired();

            //builder.Entity<Variant>().HasMany(p => p.ProductVariants).WithOne(v => v.Variant).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<ProductVariant>().ToTable("ProductVariants");
            builder.Entity<ProductVariant>().HasOne(p => p.Variant).WithMany(v => v.ProductVariants).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<ProductVariant>().HasOne(p => p.VariantOption).WithMany(v => v.ProductVariants).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CompositeProduct>().ToTable("CompositeProducts");
            builder.Entity<SKU>().ToTable("SKUs");
            builder.Entity<VariantOption>().ToTable("VariantOptions");
            builder.Entity<Variant>().ToTable("Variants");

            builder.Entity<Product>().HasData
            (
                new Product
                {
                    Id = 100,
                    Name = "Apple",
                    //QuantityInPackage = 1,
                    //UnitOfMeasurement = EUnitOfMeasurement.Unity,
                    CategoryId = 100
                },
                new Product
                {
                    Id = 101,
                    Name = "Milk",
                    //QuantityInPackage = 2,
                    //UnitOfMeasurement = EUnitOfMeasurement.Liter,
                    CategoryId = 101,
                }
            );
        }
    }
}