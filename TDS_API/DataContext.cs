using Microsoft.EntityFrameworkCore;
using TDS_API.Data;

namespace TDS_API
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }

        public DbSet<People> Peoples { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Other> Others { get; set; }
    }
}
