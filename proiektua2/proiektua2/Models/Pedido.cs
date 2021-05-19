using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Models
{
    public class Pedido
    {
        [Key]
        public int idPedido { get; set; }
        public int idUsuario { get; set; }
        public DateTime fecha { get; set; }
    }
}
