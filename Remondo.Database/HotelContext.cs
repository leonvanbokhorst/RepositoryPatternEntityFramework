using System.Data.Entity;
using Remondo.Model;

namespace Remondo.Database
{
    public class HotelContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}