using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace SIDOK.Models
{
    public class Dokter
    {
        public int Id { get; set; }

        public string Nama { get; set; }

        [ValidateNever]
        public string NIP { get; set; }
        public string NIK { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tanggal Lahir")]
        [DataType(DataType.Date)]
        public DateTime TanggalLahir { get; set; }

        public string TempatLahir { get; set; }

        public int JenisKelamin { get; set; }

        public int SpesialisasiId { get; set; }

        public Spesialisasi? spesialisasi { get; set; } = null;
    }
}
