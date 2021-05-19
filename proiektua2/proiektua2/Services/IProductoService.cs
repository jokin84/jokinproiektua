using proiektua2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Services
{
    public interface IProductoService
    {
        // GET: Nabarmendutako ardoak
        IList<Producto> NabarEskaintzak();
        IList<Producto> GustiakEskaintzak();


    }
}
