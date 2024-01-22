using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIDOK.Data;
using SIDOK.Models;
using System.Linq;

namespace SIDOK.Controllers
{
    public class JadwalController : Controller
    {
        private readonly AppDbContext _context;

        public JadwalController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string spesialisasiSearch, string poliSearch)
        {
            IQueryable<string> spesialisasiQuery = from s in _context.Spesialisasi
                                                   orderby s.Nama
                                                   select s.Nama;

            IQueryable<string> poliQuery = from p in _context.Poli
                                           orderby p.Nama
                                           select p.Nama;

            var spes = from s in _context.Spesialisasi
                       select s;

            var poli = from p in _context.Poli
                       select p;

            var dokter = from d in _context.Dokter
                       select d;
            var Jadwal = from j in _context.Jadwal_Jaga
                         select j;

            Jadwal = from j in _context.Jadwal_Jaga
                     join d in _context.Dokter on j.DokterId equals d.Id
                     join s in _context.Spesialisasi on d.SpesialisasiId equals s.Id
                     join p in _context.Poli on j.PoliId equals p.Id
                     where (string.IsNullOrEmpty(spesialisasiSearch) || s.Nama.Replace("\t", "") == spesialisasiSearch)
                            && (string.IsNullOrEmpty(poliSearch) || p.Nama.Replace("\t", "") == poliSearch)
                     select j;


            var jadwal = new JadwalJagaViewModel
            {
                Dokters = await _context.Dokter.ToListAsync(),
                Polis = await poli.ToListAsync(),
                Jadwals = await Jadwal.ToListAsync(),
                Spesialisasi = await spes.ToListAsync(),
                POLI = new SelectList(await poliQuery.Distinct().ToListAsync()),
                SPESIALISASI = new SelectList(await spesialisasiQuery.Distinct().ToListAsync()),
            };
            return View(jadwal);
        }
        public async Task<IActionResult> JagaDokter(int id)
        {
            var jaga = await _context.Jadwal_Jaga.Where(x => x.DokterId == id).ToListAsync();
            if(jaga == null)
            {
                return NotFound();
            }
            var poli = await _context.Poli.ToListAsync();

            var jadwal = new JadwalJagaViewModel
            {
                Jadwals = jaga,
                Polis = poli,
            };
            return View(jadwal);
        }

        public async Task<IActionResult> PoliDokter(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var jadwals = await _context.Jadwal_Jaga.Where(p => p.PoliId == id).ToListAsync();

            var jadwal = new JadwalJagaViewModel
            {
                Dokters = await _context.Dokter.ToListAsync(),
                Spesialisasi = await _context.Spesialisasi.ToListAsync(),
                Jadwals = jadwals,
            };

            return View(jadwal);
        }

        public async Task<IActionResult> TambahJadwal(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var dokter = await _context.Dokter.FindAsync(id);
            if (dokter == null)
            {
                return NotFound();
            }
            var poli = await _context.Poli.ToListAsync();
            if (poli == null)
            {
                return NotFound();
            }

            var jadwal = new JadwalJagaViewModel
            {
                Dokter = dokter,
                Polis = poli,
                Jadwal = new Jadwal_Jaga()
            };

            return View(jadwal);
        }

        //POST : Jadwal/TambahJadwal
        
        [HttpPost, ActionName("Tambah")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TambahJadwal([Bind("Id, DokterId, Hari, PoliId")] Jadwal_Jaga jaga)
        {

            if(ModelState.IsValid)
            {
                _context.Add(jaga);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var dokter = await _context.Dokter.FindAsync(jaga.DokterId);
            if (dokter == null)
            {
                return NotFound();
            }
            var poli = await _context.Poli.ToListAsync();
            if (poli == null)
            {
                return NotFound();
            }

            var jadwal = new JadwalJagaViewModel
            {
                Dokter = dokter,
                Polis = poli,
                Jadwal = new Jadwal_Jaga()
            };
            return Redirect($"/Jadwal/TambahJadwal/{jaga.Id}");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jadwal = await _context.Jadwal_Jaga.FindAsync(id);
            if(jadwal == null)
            {
                return NotFound();
            }

            var Jadwal = new JadwalJagaViewModel
            {
                Dokters = await _context.Dokter.ToListAsync(),
                Jadwal = jadwal,
                Polis = await _context.Poli.ToListAsync(),
            };

            return View(Jadwal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jadwal = await _context.Jadwal_Jaga.FindAsync(id);
            if (jadwal != null)
            {
                _context.Jadwal_Jaga.Remove(jadwal);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool PoliExists(int id)
        {
            return _context.Poli.Any(e => e.Id == id);
        }
    }
}
