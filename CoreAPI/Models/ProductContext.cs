using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> option) 
            : base(option)
        {}

        public DbSet<Product> ProductItems{get; set;}
    }
}