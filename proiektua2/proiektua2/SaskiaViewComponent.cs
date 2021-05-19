using Microsoft.AspNetCore.Mvc;
using proiektua2.Models;
using proiektua2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2
{
    public class SaskiaViewComponent : ViewComponent
    {
        private readonly ISaskiaService _saskiaService;

        public SaskiaViewComponent(ISaskiaService saskiaService)
        {
            _saskiaService = saskiaService;
        }
        public IViewComponentResult Invoke()
        {
            var cart = Saskia.SaskiaLortu();

            ViewData["ItemGuztiak"] = _saskiaService.ItemGuztiak(cart.SaskiaId);

            return View();
        }
    }

}
