using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIDOK.Data;
using SIDOK.Models;

namespace SIDOK.Controllers
{
    public class PoliController : Controller
    {
        private readonly AppDbContext _context;

        public PoliController(AppDbContext context) {  _context = context; }


        //Get : Poli/Index
        public async Task<IActionResult> Index()
        {
            if (_context.Poli == null)
            {
                return Problem("Entity set 'AppDbContext.Poli'  is null.");
            }

            return View(await _context.Poli.ToListAsync());
        }

        //Get : Poli/DetailPoli
        public async Task<IActionResult> DetailPoli(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var poli = await _context.Poli.FirstOrDefaultAsync(x => x.Id == id);

            return View(poli);
        }

        //GET : Poli/Tambah
        public IActionResult TambahPoli()
        {
            return View();
        }

        //POST : Poli/Tambah
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TambahPoli([Bind("Id, Nama, Lokasi")] Poli poli) 
        {
            if(ModelState.IsValid)
            {
                _context.Poli.Add(poli);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        //GET : Poli/EditPoli/1
        public async Task<IActionResult> EditPoli(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _context.Poli.FindAsync(id));
        }

        //POST : Poli/EditPoli/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPoli(int id, [Bind("Id, Nama, Lokasi")] Poli poli)
        {
            if (id != poli.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try
                {
                    _context.Poli.Update(poli);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException)
                {
                    if (!PoliExists(poli.Id))
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

            return View(await _context.Poli.FindAsync(id));
        }


        //GET : Poli/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _context.Poli.FindAsync(id));
        }

        //POST: Poli/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poli = await _context.Poli.FindAsync(id);
            if (poli != null)
            {
                _context.Remove(poli);
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
