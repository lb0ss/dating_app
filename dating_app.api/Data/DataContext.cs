using dating_app.api.Models;
using Microsoft.EntityFrameworkCore;

namespace dating_app.api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }

        public DbSet<Value> Values { get; set; }

        public DbSet<User> Users { get; set; }

        
    }
}