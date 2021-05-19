using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proiektua2;
using proiektua2.Models;
using proiektua2.Services;
using proiektua2.ViewModels;

namespace proiektua2.Controllers
{
    public class SaskiaController : Controller
    {
        private readonly AdminDbContext _context;
        private readonly ISaskiaService _saskiaService;
        private readonly IProductoService _ardoaService;
        public static Guid tempCartId ;
        public SaskiaController(AdminDbContext context,ISaskiaService saskiaService, IProductoService ardoaService)
        {
            _saskiaService = saskiaService;
            _ardoaService = ardoaService;
            _context = context;
        }

       /* public SaskiaController(AdminDbContext context)
        {
            _context = context;
        }*/

        // GET: Saskias
        public async Task<IActionResult> Index()
        {
            var cart = Saskia.SaskiaLortu();
            var saskiaViewModel = new SaskiaViewModel(); //ViewModel bat erabiliko dugu
            saskiaViewModel.SaskiaAleak = _saskiaService.SaskiaLortuAleak(cart.SaskiaId);
            saskiaViewModel.SaskiaGuztira = _saskiaService.Guztira(cart.SaskiaId);

            return View(saskiaViewModel);


            
        }
      
        public IActionResult SaskiaGehitu(int id)
        {
            var cart = Saskia.SaskiaLortu(); //aurretik sortu dugun Saskia klasea erabiliz
            _saskiaService.SaskiaGehitu(id, cart.SaskiaId); //zerbitzu berrian karritoan gehitzeko
            return RedirectToAction("Index");
        }
        public IActionResult SaskiaGehituJson(int id)
        {
            var cart = Saskia.SaskiaLortu();

            var saskiaItem = _saskiaService.SaskiaAleaDatuak(id, cart.SaskiaId);

            _saskiaService.SaskiaGehitu(id, cart.SaskiaId);

            var results = new SaskiaKenduViewModel
            {
                Mezua = HtmlEncoder.Default.Encode(saskiaItem.Producto.Nombre) + " zure saskira gehitu da",
                SaskiaGuztira = _saskiaService.Guztira(cart.SaskiaId),
                //ItemGuztiak = _saskiaService.ItemGuztiak(cart.SaskiaId),
                ItemKopurua = saskiaItem.Kantitatea,
                EzabatutakoId = id
            };

            return Json(results);
        }
        [HttpPost]
        public ActionResult SaskiaKendu(int id)
        {
            var cart = Saskia.SaskiaLortu();

            var saskiaItem = _saskiaService.SaskiaAleaDatuak(id, cart.SaskiaId);

            int itemCount = _saskiaService.SaskiaKendu(id, cart.SaskiaId);

            var results = new SaskiaKenduViewModel
            {
                Mezua = HtmlEncoder.Default.Encode(saskiaItem.Producto.Nombre) + " zure saskitik kendu da",
                SaskiaGuztira = _saskiaService.Guztira(cart.SaskiaId),
                ///ItemGuztiak = _saskiaService.ItemGuztiak(cart.SaskiaId),
                ItemKopurua = itemCount,
                EzabatutakoId = id
            };

            return Json(results);
        }

        // GET: Saskias/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saskia = await _context.Saskia
                .FirstOrDefaultAsync(m => m.SaskiaId == id);
            if (saskia == null)
            {
                return NotFound();
            }

            return View(saskia);
        }

        // GET: Saskias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Saskias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaskiaId")] Saskia saskia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saskia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saskia);
        }

        // GET: Saskias/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saskia = await _context.Saskia.FindAsync(id);
            if (saskia == null)
            {
                return NotFound();
            }
            return View(saskia);
        }

        // POST: Saskias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SaskiaId")] Saskia saskia)
        {
            if (id != saskia.SaskiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saskia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaskiaExists(saskia.SaskiaId))
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
            return View(saskia);
        }

        // GET: Saskias/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saskia = await _context.Saskia
                .FirstOrDefaultAsync(m => m.SaskiaId == id);
            if (saskia == null)
            {
                return NotFound();
            }

            return View(saskia);
        }

        // POST: Saskias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var saskia = await _context.Saskia.FindAsync(id);
            _context.Saskia.Remove(saskia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


private bool SaskiaExists(string id)
        {
            return _context.Saskia.Any(e => e.SaskiaId == id);
        }
    }
}
