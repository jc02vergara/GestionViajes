using GestionViajes.Data;
using GestionViajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionViajes.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ReservasController(ApplicationDbContext db) => _db = db;

        private async Task LoadDropdownsAsync(int? destinoId = null, int? turistaId = null)
        {
            ViewBag.Destinos = new SelectList(await _db.Destinos.OrderBy(d => d.Nombre).ToListAsync(), "DestinoId", "Nombre", destinoId);
            ViewBag.Turistas = new SelectList(await _db.Turistas.OrderBy(t => t.Apellido).ThenBy(t => t.Nombre).ToListAsync(), "TuristaId", "Apellido", turistaId);
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 8;
            var q = _db.Reservas.Include(r => r.Destino).Include(r => r.Turista).OrderByDescending(r => r.FechaReserva);
            var total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);
            return View(items);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var r = await _db.Reservas.Include(x => x.Destino).Include(x => x.Turista).FirstOrDefaultAsync(x => x.ReservaId == id);
            if (r == null) return NotFound();
            return View(r);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View(new Reserva());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DestinoId,TuristaId,FechaReserva,FechaViaje,Personas,Estado")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                var destino = await _db.Destinos.FindAsync(reserva.DestinoId);
                if (destino != null) reserva.Total = destino.Costo * reserva.Personas;
                _db.Reservas.Add(reserva);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadDropdownsAsync(reserva.DestinoId, reserva.TuristaId);
            return View(reserva);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var r = await _db.Reservas.FindAsync(id);
            if (r == null) return NotFound();
            await LoadDropdownsAsync(r.DestinoId, r.TuristaId);
            return View(r);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservaId,DestinoId,TuristaId,FechaReserva,FechaViaje,Personas,Total,Estado")] Reserva reserva)
        {
            if (id != reserva.ReservaId) return NotFound();
            if (ModelState.IsValid)
            {
                var destino = await _db.Destinos.FindAsync(reserva.DestinoId);
                if (destino != null) reserva.Total = destino.Costo * reserva.Personas;
                _db.Update(reserva);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadDropdownsAsync(reserva.DestinoId, reserva.TuristaId);
            return View(reserva);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var r = await _db.Reservas.Include(x => x.Destino).Include(x => x.Turista).FirstOrDefaultAsync(x => x.ReservaId == id);
            if (r == null) return NotFound();
            return View(r);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var r = await _db.Reservas.FindAsync(id);
            if (r != null) { _db.Reservas.Remove(r); await _db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}