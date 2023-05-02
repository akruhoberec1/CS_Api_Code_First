global using Microsoft.EntityFrameworkCore;

namespace API_CSharp
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 

        }

        public DbSet<VehicleMake> VehicleMakes => Set<VehicleMake>();


    }
}
