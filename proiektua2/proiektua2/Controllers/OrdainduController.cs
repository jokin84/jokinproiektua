using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proiektua2.Models;
using proiektua2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Controllers
{
    public class OrdainduController : Controller
    {
        private readonly AdminDbContext _context;
        private readonly ISaskiaService _saskiaService;
        public OrdainduController(AdminDbContext context, ISaskiaService saskiaService)
        {
            _saskiaService = saskiaService;
            _context = context;
        }
        public IActionResult Index()
        {
            ///var bezeroa = _saskiaService.BezeroaDatuak(HttpContext.User.Identity.Name);
            var bezeroa = _saskiaService.BezeroaDatuak(AdminDbContext.user.idusuario);
            return View();
        }
        public async Task<IActionResult> pedidosUsuarios()
        {
            return View(await _context.Pedidos.ToListAsync());
        }
        /*  [HttpPost]
          [ValidateAntiForgeryToken]
          public IActionResult Index([Bind("Abizena,Helbidea,Herrialdea,Hiria,Izena,Postakodea,Telefonoa")] BezeroaEskaera bezeroaEskaera)
          {
              if (ModelState.IsValid)
              {
                 ////// Bezeroen datuak gorde
                  bezeroaEskaera.Erabiltzailea = HttpContext.User.Identity.Name;
                  bezeroaEskaera.Data = DateTime.Now;
                  _saskiaService.EskaeraBezeroaGehitu(bezeroaEskaera);

                  /////////Eskaera gorde
                  var cart = Saskia.SaskiaLortu();
                  _saskiaService.EskaeraSortu(bezeroaEskaera, cart.SaskiaId);
                  return RedirectToAction("Osatu");
              }
              return View(bezeroaEskaera);
          }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(BezeroaEskaera bezeroaEskaera)
        {
           
            if (ModelState.IsValid)
            {
               /* try
                {*/
                    bezeroaEskaera.Erabiltzaileaid = AdminDbContext.user.idusuario;
                    bezeroaEskaera.Data = DateTime.Now;
                    var cart = Saskia.SaskiaLortu();
               
                /*Bezeroa konprobatu*/
                if (_saskiaService.BezeroaKonprobatu(bezeroaEskaera.Erabiltzaileaid))
                    {
                        _saskiaService.BezeroaAldatu(bezeroaEskaera);
                    }
                    else
                    {
                        _saskiaService.EskaeraBezeroaGehitu(bezeroaEskaera);
                    }
                    _saskiaService.ErosketaGehitu(bezeroaEskaera, cart.SaskiaId);
                    _saskiaService.SaskiaEzabatu(cart.SaskiaId);

                    return RedirectToAction("Osatu", new { id = bezeroaEskaera.Id });
                /* }
                 catch (Exception ex)
                 {
                     ModelState.AddModelError("errorea", ex.Message);
                 }*/
            }
            return View(bezeroaEskaera);
        }
    
        public IActionResult Osatu(int id)
        {
            ViewData["bezeroaId"] = id;
            return View();
        }
    
    }
}

   