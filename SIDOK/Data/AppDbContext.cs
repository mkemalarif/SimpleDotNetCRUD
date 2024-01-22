using Microsoft.EntityFrameworkCore;

namespace SIDOK.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        
        }

        public DbSet<SIDOK.Models.Spesialisasi> Spesialisasi { get; set; }
        public DbSet<SIDOK.Models.Poli> Poli { get; set; }
        public DbSet<SIDOK.Models.Dokter> Dokter { get; set; }
        public DbSet<SIDOK.Models.Jadwal_Jaga> Jadwal_Jaga { get; set; }
    }
}
