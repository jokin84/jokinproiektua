using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Models
{
    public class Marca
    {
        [Key]
        public int id { get; set; }
        public String Nombre { get; set; }
        public virtual IList<Producto> Productos { get; set; }
        public Marca(int id, string nombre)
        {
            this.id = id;
            Nombre = nombre;
        }
        public Marca()
        {
        }

    }
}
