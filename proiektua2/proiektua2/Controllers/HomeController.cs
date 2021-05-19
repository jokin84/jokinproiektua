using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using proiektua2.Models;
using proiektua2.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductoService _productoService;


        public HomeController(ILogger<HomeController> logger, IProductoService productoService)
        {
            ///_logger = logger;
            _productoService = productoService;
        }
        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {
            return View(_productoService.GustiakEskaintzak());
        }
        public IActionResult Ezarpenak()
        {
            return View(_productoService.GustiakEskaintzak());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
