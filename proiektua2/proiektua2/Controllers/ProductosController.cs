using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proiektua2;
using proiektua2.Models;
using proiektua2.Services;

namespace proiektua2.Controllers
{
    public class ProductosController : Controller
    {
        private readonly AdminDbContext _context;
        private readonly IProductoService _productoService;
        public ProductosController(AdminDbContext context, IProductoService productoService)
        {
            _productoService = productoService;
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var adminDbContext = _context.Productos.Include(p => p.Categoria).Include(p => p.Marca);
            return View(await adminDbContext.ToListAsync());
        }
        public async Task<IActionResult> prueba()
        {
            
            return View();
        }
        public async Task<IActionResult> produktoKategoria()
        {

            return View(_productoService.GustiakEskaintzak());
        }
        public async Task<IActionResult> produktoMarka()
        {

            return View(_productoService.GustiakEskaintzak());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["Categoriaid"] = new SelectList(_context.Categoria, "id", "id");
            ViewData["Marcaid"] = new SelectList(_context.Marcas, "id", "id");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idProductos,Marcaid,Categoriaid,descripcion,precio,precioTotal,imagen,Eskaintza,Deskontua")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categoriaid"] = new SelectList(_context.Categoria, "id", "id", producto.Categoriaid);
            ViewData["Marcaid"] = new SelectList(_context.Marcas, "id", "id", producto.Marcaid);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["Categoriaid"] = new SelectList(_context.Categoria, "id", "id", producto.Categoriaid);
            ViewData["Marcaid"] = new SelectList(_context.Marcas, "id", "id", producto.Marcaid);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idProductos,Marcaid,Categoriaid,descripcion,precio,precioTotal,imagen,Eskaintza,Deskontua")] Producto producto)
        {
            if (id != producto.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.id))
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
            ViewData["Categoriaid"] = new SelectList(_context.Categoria, "id", "id", producto.Categoriaid);
            ViewData["Marcaid"] = new SelectList(_context.Marcas, "id", "id", producto.Marcaid);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.id == id);
        }

        public Marca getIdmarca(int id)
        {


            var marca = _context.Marcas
                .FirstOrDefaultAsync(m => m.id == id);


            return marca.Result;
        }
        public Categoria getIdcategoria(int id)
        {


            var marca = _context.Categoria
                .FirstOrDefaultAsync(m => m.id == id);


            return marca.Result;
        }
        public async Task<IActionResult> categoriaProductuak(int id)
        {
            ViewBag.id = id;
            var adminDbContext = _context.Productos.Include(p => p.Categoria).Include(p => p.Marca);
            return View(await adminDbContext.ToListAsync());
        }
        public async Task<IActionResult> MarcaProductuak(int id)
        {
            ViewBag.id = id;
            var adminDbContext = _context.Productos.Include(p => p.Categoria).Include(p => p.Marca);
            return View(await adminDbContext.ToListAsync());
        }
        public async Task<IActionResult> detatellak2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /* var pro = await _context.Productos
                 //.Include(a => a.Marca)

                 .FirstOrDefaultAsync(m => m.idProductos == id);*/
            var Contextproductos = (from Productos in _context.Productos
                                    join Marcas in _context.Marcas
                                    on Productos.Marcaid equals Marcas.id
                                    join Categoria in _context.Categoria
                                    on Productos.Categoriaid equals Categoria.id
                                    select new Producto
                                    {
                                        id = Productos.id,
                                        Marcaid = Productos.Marcaid,
                                        Marca = new Marca(Productos.Marcaid, Marcas.Nombre),
                                        Categoriaid = Productos.Categoriaid,
                                        Categoria = new Categoria(Productos.Categoriaid, Categoria.Nombre),
                                        descripcion = Productos.descripcion,
                                        precio = Productos.precio,
                                        precioTotal = Productos.precioTotal,
                                        imagen = Productos.imagen,
                                        Eskaintza = Productos.Eskaintza,
                                        Deskontua = Productos.Deskontua,

                                    }).FirstOrDefaultAsync(m => m.id == id);

            Producto pro = new Producto(Contextproductos.Result.id, Contextproductos.Result.Marcaid, Contextproductos.Result.Marca, Contextproductos.Result.Categoriaid, Contextproductos.Result.Categoria, Contextproductos.Result.descripcion, Contextproductos.Result.precio, Contextproductos.Result.precioTotal, Contextproductos.Result.imagen, Contextproductos.Result.Eskaintza, Contextproductos.Result.Deskontua);

            return View(await Contextproductos);
            /*if (pro == null)
            {
                return NotFound();
            }

            return View(pro);*/
        }
    }
}
