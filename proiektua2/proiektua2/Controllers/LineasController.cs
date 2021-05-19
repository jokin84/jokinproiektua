using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proiektua2;
using proiektua2.Models;

namespace proiektua2.Controllers
{
    public class LineasController : Controller
    {
        private readonly AdminDbContext _context;

        public LineasController(AdminDbContext context)
        {
            _context = context;
        }

        // GET: Lineas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Linea.ToListAsync());
        }

        // GET: Lineas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linea = await _context.Linea
                .FirstOrDefaultAsync(m => m.idLinea == id);
            if (linea == null)
            {
                return NotFound();
            }

            return View(linea);
        }

        // GET: Lineas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lineas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idLinea,idPedido,idProducto,numlineas,unidades,precio")] Linea linea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(linea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(linea);
        }

        // GET: Lineas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linea = await _context.Linea.FindAsync(id);
            if (linea == null)
            {
                return NotFound();
            }
            return View(linea);
        }

        // POST: Lineas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idLinea,idPedido,idProducto,numlineas,unidades,precio")] Linea linea)
        {
            if (id != linea.idLinea)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(linea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineaExists(linea.idLinea))
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
            return View(linea);
        }

        // GET: Lineas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linea = await _context.Linea
                .FirstOrDefaultAsync(m => m.idLinea == id);
            if (linea == null)
            {
                return NotFound();
            }

            return View(linea);
        }

        // POST: Lineas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var linea = await _context.Linea.FindAsync(id);
            _context.Linea.Remove(linea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineaExists(int id)
        {
            return _context.Linea.Any(e => e.idLinea == id);
        }
    }
}
