using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Models
{
    public class SaskiaAlea
    {
        [Key]
        public int Id { get; set; }

        public string SaskiaId { get; set; }

        public int Kantitatea { get; set; }

        public System.DateTime Data { get; set; }

        public int ProductoId { get; set; }

        public virtual Producto Producto { get; set; }
    }

}
