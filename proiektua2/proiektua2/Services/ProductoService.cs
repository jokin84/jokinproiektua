using proiektua2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AdminDbContext _context;

        public ProductoService(AdminDbContext context)
        {
            _context = context;
        }
        // GET: Nabarmendutako ardoak
        public IList<Producto> NabarEskaintzak()
        {
            var wineShopDbContext = _context.Productos
                .Where(a => a.Eskaintza == true)
                ;
            /*
             *var wineShopDbContext = _context.Ardoa
                .Where(a => a.Eskaintza == true)
                .Include(a => a.Mota)
                .Include(a => a.Upeltegia);
                */
            return wineShopDbContext.ToList();
        }

        public IList<Producto> GustiakEskaintzak()
        {
            /*var wineShopDbContext = _context.Productos
                .Include(a => a.Marca)

                .Select(a => a);*/
            var Contextproductos = (from Productos in _context.Productos
                                    join Marcas in _context.Marcas
                                    on Productos.Marcaid equals Marcas.id
                                    join Categoria in _context.Categoria
                                    on Productos.Categoriaid equals Categoria.id
                                    select new
                                    {
                                        Productos.id,
                                        Productos.Marcaid,
                                        Marca = new Marca(Productos.Marcaid, Marcas.Nombre),
                                        Productos.Categoriaid,
                                        Categoria = new Categoria(Productos.Categoriaid, Categoria.Nombre),
                                        Productos.descripcion,
                                        Productos.precio,
                                        Productos.imagen,
                                        Productos.Eskaintza,
                                        Productos.Deskontua,

                                    }).ToList();
            List<Producto> CopyCliente = new List<Producto>();
            foreach (var element in Contextproductos)
            {
                CopyCliente.Add(new Producto(element.id, element.Marcaid, element.Marca, element.Categoriaid, element.Categoria, element.descripcion, element.precio, element.precio, element.imagen, element.Eskaintza, element.Deskontua));
            }
            return CopyCliente;
            ///return null;
        }

    }
}
