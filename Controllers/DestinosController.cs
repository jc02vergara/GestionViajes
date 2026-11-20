using GestionViajes.Data;
using GestionViajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionViajes.Controllers
{
    public class DestinosController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DestinosController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            const int pageSize = 5;
            var q = _db.Destinos.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(d => d.Nombre.Contains(search) || d.Pais.Contains(search));
            var total = await q.CountAsync();
            var items = await q.OrderBy(d => d.Nombre).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);
            return View(items);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var d = await _db.Destinos.FindAsync(id);
            if (d == null) return NotFound();
            return View(d);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Pais,Descripcion,Costo")] Destino destino)
        {
            if (ModelState.IsValid)
            {
                _db.Destinos.Add(destino);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(destino);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var d = await _db.Destinos.FindAsync(id);
            if (d == null) return NotFound();
            return View(d);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DestinoId,Nombre,Pais,Descripcion,Costo")] Destino destino)
        {
            if (id != destino.DestinoId) return NotFound();
            if (ModelState.IsValid)
            {
                _db.Update(destino);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(destino);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var d = await _db.Destinos.FindAsync(id);
            if (d == null) return NotFound();
            return View(d);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var d = await _db.Destinos.FindAsync(id);
            if (d != null) { _db.Destinos.Remove(d); await _db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}