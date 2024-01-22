using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIDOK.Models
{
    public class Jadwal_Jaga
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Hari { get; set; }

        public int PoliId { get; set; }

        public Poli? Poli { get; set; } = null;

        public int DokterId { get; set; }

        public Dokter? Dokter { get; set; } = null;
    }
}
