using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Models
{
    public class Linea
    {
        [Key]
        public int idLinea { get; set; }
        [ForeignKey("Pedidos")]
        [Column(Order = 1)]
        public int idPedido { get; set; }
        [ForeignKey("Producto")]
        [Column(Order = 2)]
        public int idProducto { get; set; }
        public int numlineas { get; set; }
        public int unidades { get; set; }
        public float precio { get; set; }
    }
}
