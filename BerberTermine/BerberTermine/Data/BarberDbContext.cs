using BerberTermine.Models;

using Microsoft.EntityFrameworkCore;

namespace BarberTermine.Models
{
    public class BarberDbContext : DbContext
    {
        public BarberDbContext(DbContextOptions<BarberDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
