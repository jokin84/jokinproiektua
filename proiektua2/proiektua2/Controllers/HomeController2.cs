                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proiektua2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Controllers
{
    public class HomeController2 : Controller
    {
        private readonly AdminDbContext _context;

        public HomeController2(AdminDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
            ///return  RedirectToAction("Edit");
        }
        public async Task<IActionResult> Edit()
        {
            int id = AdminDbContext.user.idusuario;
     

       
            return View();
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("erabiltzeId,PaswordOld,Pasword1,Pasword2")] pasahitzaAldatu aldatu)
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
                return RedirectToAction(nameof(Index));
            
            //return View(aldatu);
        }
    }
}
