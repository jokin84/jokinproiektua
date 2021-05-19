using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proiektua2.Controllers;
using proiektua2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Services
{
    public class SaskiaService : ISaskiaService
    {
        private readonly AdminDbContext _context;

        public SaskiaService(AdminDbContext context)
        {
            _context = context;
        }
        public void SaskiaGehitu(int ardoaId, string saskiaId)
        {
            var cartitem = _context.SaskiaAlea.SingleOrDefault(
                s => s.SaskiaId == saskiaId
                  && s.ProductoId == ardoaId
                );

            if (cartitem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartitem = new SaskiaAlea
                {
                    ProductoId = ardoaId,
                    SaskiaId = saskiaId,
                    Kantitatea = 1,
                    Data = DateTime.Now
                };
                _context.SaskiaAlea.Add(cartitem);
            }
            else
            {
                cartitem.Kantitatea++;
            }
            _context.SaveChanges();
        }


        public IList<SaskiaAlea> SaskiaLortuAleak(string saskiaId)
        {
            return _context.SaskiaAlea
                .Where(s => s.SaskiaId == saskiaId)
                .Include(s => s.Producto)
                .ToList();
        }
        public decimal Guztira(string saskiaId)
        {
            decimal? total = (decimal?)_context.SaskiaAlea
                            .Where(s => s.SaskiaId == saskiaId)
                            .Select(s => s.Kantitatea * (s.Producto.precio - (s.Producto.precio * s.Producto.Deskontua / 100))).Sum();
            return total ?? 0;
        }
        public SaskiaAlea SaskiaAleaDatuak(int ardoaId, string saskiaId)
        {
            var cartItem = _context.SaskiaAlea
                 .Include(s => s.Producto)
                 .SingleOrDefault(
                     c => c.SaskiaId == saskiaId
                     && c.ProductoId == ardoaId);

            return cartItem;
        }
        public int SaskiaKendu(int ardoaId, string saskiaId)
        {
            var cartItem = _context.SaskiaAlea.SingleOrDefault(
                            c => c.SaskiaId == saskiaId
                            && c.ProductoId == ardoaId);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Kantitatea > 1)
                {
                    cartItem.Kantitatea--;
                    itemCount = cartItem.Kantitatea;
                }
                else
                {
                    _context.SaskiaAlea.Remove(cartItem);
                }

                _context.SaveChanges();
            }
            return itemCount;
        }
        public int ItemGuztiak(string saskiaId)
        {
            int? count = _context.SaskiaAlea
                .Where(s => s.SaskiaId == saskiaId)
                .Select(s => s.Kantitatea).Sum();

            return count ?? 0;
        }

        public void EskaeraBezeroaGehitu(BezeroaEskaera bezeroaEskaera)
        {
            _context.BezeroaEskaera.Add(bezeroaEskaera);
        }
        public void EskaeraSortu(BezeroaEskaera bezeroaEskaera, string saskiaId)
        {
            var cartItems = SaskiaLortuAleak(saskiaId);
            int id = BezeroaEskaeraLortu(bezeroaEskaera);
            bezeroaEskaera.Id = id;
            foreach (var item in cartItems)
            {
                var erosketa = new Erosketa
                {
                    ProductoId = item.ProductoId,
                    BezeroaEskaeraId = bezeroaEskaera.Id,
                    Kantitatea = item.Kantitatea
                };
                _context.Erosketa.Add(erosketa);
            }
            _context.SaveChanges();
            SaskiaEzabatu(saskiaId);
        }
        public int BezeroaEskaeraLortu(BezeroaEskaera bezeroaEskaera)
        {
            BezeroaEskaera zaharra = _context.BezeroaEskaera
                .SingleOrDefault(b => b.Erabiltzaileaid == bezeroaEskaera.Erabiltzaileaid);
            return zaharra.Id;
        }
        public void SaskiaEzabatu(string saskiaId)
        {
            var cartItems = _context.SaskiaAlea.Where(c => c.SaskiaId == saskiaId);

            foreach (var cartItem in cartItems)
            {
                _context.SaskiaAlea.Remove(cartItem);
            }
            _context.SaveChanges();
        }
        public bool BezeroaKonprobatu(int erabiltzailea)
        {
            return _context.BezeroaEskaera.Any(b => b.Erabiltzaileaid == erabiltzailea);
        }
        public BezeroaEskaera BezeroaDatuak(int erabiltzailea)
        {
            var bezeroaEskaera = _context.BezeroaEskaera
                .SingleOrDefault(b => b.Erabiltzaileaid == erabiltzailea);
            return bezeroaEskaera;
        }
        public void BezeroaAldatu(BezeroaEskaera bezeroaEskaera)
        {
            BezeroaEskaera zaharra = _context.BezeroaEskaera.SingleOrDefault(b => b.Izena == bezeroaEskaera.Izena);

            zaharra.Izena = bezeroaEskaera.Izena;
            zaharra.Abizena = bezeroaEskaera.Abizena;
            zaharra.Data = bezeroaEskaera.Data;
            zaharra.Helbidea = bezeroaEskaera.Helbidea;
            zaharra.Hiria = bezeroaEskaera.Hiria;
            zaharra.Herrialdea = bezeroaEskaera.Herrialdea;
            zaharra.Postakodea = bezeroaEskaera.Postakodea;
            zaharra.Telefonoa = bezeroaEskaera.Telefonoa;
            _context.Update(zaharra);
            _context.SaveChanges();
        }
        public int ErosketaGehitu(BezeroaEskaera bezeroaEskaera, string saskiaId)
        {
            var jarraipena = JarraipenaLortu();
            jarraipena++;

            var cartItems = SaskiaLortuAleak(saskiaId);
            int id = BezeroaEskaeraLortu(bezeroaEskaera);
            bezeroaEskaera.Id = id;
            foreach (var item in cartItems)
            {
                var erosketa = new Erosketa
                {
                    BezeroaEskaeraId = id,
                    ProductoId = item.ProductoId,
                    Kantitatea = item.Kantitatea,
                    Data = DateTime.Now,
                    Jarraipena = jarraipena
                };
                _context.Add(erosketa);
            }
            _context.SaveChanges();
            return jarraipena;
        }
        public int JarraipenaLortu()
        {
            return _context.Erosketa
                .Max(e => e.Jarraipena);
        }

    }
}
