using GestionViajes.Data;
using GestionViajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionViajes.Controllers
{
    public class TuristasController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TuristasController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            const int pageSize = 5;
            var q = _db.Turistas.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(t => t.Nombre.Contains(search) || t.Apellido.Contains(search) || t.Email.Contains(search));
            var total = await q.CountAsync();
            var items = await q.OrderBy(t => t.Apellido).ThenBy(t => t.Nombre).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);
            return View(items);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var t = await _db.Turistas.FindAsync(id);
            if (t == null) return NotFound();
            return View(t);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,Email,Telefono")] Turista turista)
        {
            if (ModelState.IsValid)
            {
                _db.Turistas.Add(turista);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turista);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var t = await _db.Turistas.FindAsync(id);
            if (t == null) return NotFound();
            return View(t);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TuristaId,Nombre,Apellido,Email,Telefono")] Turista turista)
        {
            if (id != turista.TuristaId) return NotFound();
            if (ModelState.IsValid)
            {
                _db.Update(turista);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turista);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var t = await _db.Turistas.FindAsync(id);
            if (t == null) return NotFound();
            return View(t);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var t = await _db.Turistas.FindAsync(id);
            if (t != null) { _db.Turistas.Remove(t); await _db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}