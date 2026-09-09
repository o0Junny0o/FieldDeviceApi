using FieldDevice.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldDevice.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Incident> Incidents { get; set; }
    }
}
