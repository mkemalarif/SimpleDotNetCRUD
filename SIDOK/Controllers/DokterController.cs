using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SIDOK.Data;
using SIDOK.Models;

namespace SIDOK.Controllers
{
    public class DokterController : Controller
    {
        private readonly AppDbContext _context;

        public DokterController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (_context.Dokter == null)
            {
                return Problem("Entity set 'AppDbContext.Dokter'  is null.");
            }

            var dokterSpesialis = new DokterSpesialisViewModel
            {
                Spesialisasis = await _context.Spesialisasi.ToListAsync(),
                Dokters = await _context.Dokter.ToListAsync(),
            };
            return View(dokterSpesialis);
        }

        //get tambah dokter
        public async Task<IActionResult> TambahDokter() {

            var DokterSpesialis = new DokterSpesialisViewModel
            {
                Spesialisasis = await _context.Spesialisasi.ToListAsync(),
                Dokter = new Dokter()
            };

            return View(DokterSpesialis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TambahDokter([Bind("Id, Nama, NIK, TanggalLahir, TempatLahir, JenisKelamin, SpesialisasiId")] Dokter dokter)
        {
            int panjangKarakterHuruf = 2;
            int panjangKarakterAngka = 11;
            string karakterHuruf = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string karakterAngka = "0123456789";

            Random random = new Random();

            string hurufString = new string(Enumerable.Range(1, panjangKarakterHuruf)
                                               .Select(_ => karakterHuruf[random.Next(karakterHuruf.Length)])
                                               .ToArray());

            string angkaString = new string(Enumerable.Range(1, panjangKarakterAngka)
                                               .Select(_ => karakterAngka[random.Next(karakterAngka.Length)])
                                               .ToArray());

            string randomString = angkaString + hurufString;

            dokter.NIP = randomString;
            if (ModelState.IsValid)
            {
                _context.Add(dokter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var DokterSpesialis = new DokterSpesialisViewModel
            {
                Spesialisasis = await _context.Spesialisasi.ToListAsync(),
                Dokter = new Dokter()
            };

            return View(DokterSpesialis);
        }

        //get detail, route = "Dokter/DetailDokter/1"
        public async Task<IActionResult> DetailDokter(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            //Dokter dokter = await _context.Dokter.FindAsync(id);

            //if (dokter == null)
            //{
            //    return NotFound();
            //}
            var dokter = await _context.Dokter.FirstOrDefaultAsync(d => d.Id == id);
            if(dokter == null)
            {
                return NotFound();
            }

            var dokterSpesialis = new DokterSpesialisViewModel
            {
                Dokter = dokter,
                Spesialisasis = await _context.Spesialisasi.ToListAsync()
            };

            return View(dokterSpesialis);
        }

        //GET EDIT : Dokter/EditDokter/1
        public async Task<IActionResult> EditDokter(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dokter = await _context.Dokter.FindAsync(id);
            if (dokter == null) { return NotFound(); }

            var spesialis = await _context.Spesialisasi.ToListAsync();
            if(spesialis.Count == 0) { return NotFound(); }

            var dokterSpesialis = new DokterSpesialisViewModel
            {
                Dokter = dokter,
                Spesialisasis = spesialis
            };

            return View(dokterSpesialis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDokter(int id, [Bind("Id, Nama, NIK, NIP, TanggalLahir, TempatLahir, JenisKelamin, SpesialisasiId")] Dokter dokter)
        {
            if (id != dokter.Id) 
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(dokter);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {

                    if (!DokterExists(dokter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            var dokterr = await _context.Dokter.FindAsync(id);
            if (dokterr == null) { return NotFound(); }

            var spesialis = await _context.Spesialisasi.ToListAsync();
            if (spesialis.Count == 0) { return NotFound(); }

            var dokterSpesialis = new DokterSpesialisViewModel
            {
                Dokter = dokterr,
                Spesialisasis = spesialis
            };

            return View(dokterSpesialis);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var dokterr = await _context.Dokter.FindAsync(id);
            if (dokterr == null) { return NotFound(); }

            var spesialis = await _context.Spesialisasi.ToListAsync();
            if (spesialis.Count == 0) { return NotFound(); }

            var dokterSpesialis = new DokterSpesialisViewModel
            {
                Dokter = dokterr,
                Spesialisasis = spesialis
            };

            return View(dokterSpesialis);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dokter = await _context.Dokter.FindAsync(id);
            if(dokter != null) 
            {
                _context.Dokter.Remove(dokter);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DokterExists(int id)
        {
            return _context.Dokter.Any(e => e.Id == id);
        }
    }
}
