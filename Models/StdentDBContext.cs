using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproach.Models
{
    public class StdentDBContext : DbContext
    {
        public StdentDBContext(DbContextOptions options) : base(options)
        {
                
        }
        public DbSet<Student> Students { get; set; }  
    }
}
