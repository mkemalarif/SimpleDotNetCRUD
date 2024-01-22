using Microsoft.AspNetCore.Mvc.Rendering;

namespace SIDOK.Models
{
    public class JadwalJagaViewModel
    {
        public Dokter? Dokter { get; set; }
        public Poli? Poli { get; set; }

        public List<Dokter>? Dokters { get; set; }

        public List<Poli>? Polis { get; set; }

        public SelectList? POLI {  get; set; }

        public Jadwal_Jaga? Jadwal { get; set; }

        public List<Jadwal_Jaga>? Jadwals { get; set; }
        public List<Spesialisasi>? Spesialisasi { get; set; }
        public SelectList? Spesialis { get; set; }
        public SelectList? SPESIALISASI { get; set; }

        public string? poliSearch { get; set; }

        public string? spesialisasiSearch {  get; set; }
    }
}
