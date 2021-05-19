using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proiektua2;
using proiektua2.Models;

namespace proiektua2.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AdminDbContext _context;
      
        public UsuariosController(AdminDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }
        public async Task<IActionResult> ErabiltzaileaErosketa()
        {

            var Contextproductos = (from Erosketa in _context.Erosketa
                                    join Productos in _context.Productos
                                    on Erosketa.ProductoId equals Productos.id
                                    join BezeroaEskaera in _context.BezeroaEskaera
                                    on Erosketa.BezeroaEskaeraId equals BezeroaEskaera.Id
                                    select new Erosketa
                                    {

                                        Id = Erosketa.Id,
                                        Kantitatea = Erosketa.Kantitatea,

                                        BezeroaEskaeraId = Erosketa.BezeroaEskaeraId,
                                        BezeroaEskaera = new BezeroaEskaera(BezeroaEskaera.Id, BezeroaEskaera.Izena, BezeroaEskaera.Erabiltzaileaid),
                                        ProductoId = Erosketa.ProductoId,
                                        Producto = new Producto(Productos.id, Productos.Nombre, Productos.descripcion, Productos.precioTotal, Productos.imagen),
                                        dirua = Productos.precioTotal * Erosketa.Kantitatea,
                                        Data = Erosketa.Data

                                    }).ToListAsync();
              /*ist<Erosketa> CopyCliente = new List<Erosketa>();
              foreach (var element in Contextproductos)
              {
                  CopyCliente.Add(new Erosketa(element.Id, element.Kantitatea, element.BezeroaEskaeraId, element.BezeroaEskaera, element.ProductoId, element.Producto, element.Data));
              }*/
              return View(await Contextproductos);
           
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.idusuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("usuario,idusuario,password,tipo,RememberMe")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("usuario,idusuario,password,tipo,RememberMe")] Usuario usuario)
        {
            if (id != usuario.idusuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.idusuario))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.idusuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.idusuario == id);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(Usuario user)
        {
            SaskiaController.tempCartId = Guid.NewGuid();
            var myUser = _context.Usuarios
            .FirstOrDefault(u => u.usuario == user.usuario
                 && u.password == user.password );
            if (myUser != null)    //User was found
            {
                AdminDbContext.sesion = true;
                AdminDbContext.user = myUser;
                return RedirectToAction("index", "Home");
            }
            else    //User was not found
            {
                return View();
            }


        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
           // SaskiaController.tempCartId. = 0;
            AdminDbContext.sesion = false;
            AdminDbContext.user = new Usuario();
            return RedirectToAction("index", "Home");
        }

        public IActionResult pasahitzaAldatu()
        {
        
            return View();
        }
        public IActionResult registrar()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> registrar([Bind("us,idusuario,password,RememberMe")] RegisterModel usuario)
        {
            Usuario us = new Usuario(usuario.us, usuario.idusuario, usuario.password, usuario.RememberMe);
            if (ModelState.IsValid)
            {
                _context.Add(us);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }
        // POST: Marcas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> pasahitzaAldatu(int id, [Bind("erabiltzeId,PaswordOld,Pasword1,Pasword2")] pasahitzaAldatu aldatu)
        {
            Usuario user = AdminDbContext.user;


            try
            {
                user.password = aldatu.Pasword2;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return RedirectToAction("","");

            //return View(aldatu);
        }
    }
}
