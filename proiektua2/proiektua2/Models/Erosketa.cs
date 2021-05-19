using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Models
{
    public class Erosketa
    {
   

        [Key]
        public int Id { get; set; }
        public int Kantitatea { get; set; }
        public int BezeroaEskaeraId { get; set; }
        public virtual BezeroaEskaera BezeroaEskaera { get; set; }
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
        public DateTime Data { get; set; }
        public int Jarraipena { get; set; }
        public float dirua { get; set; }
        public Erosketa() { }
        public Erosketa(int id, int kantitatea, int bezeroaEskaeraId, BezeroaEskaera bezeroaEskaera, int productoId, Producto producto, DateTime data)
        {
            Id = id;
            Kantitatea = kantitatea;
            BezeroaEskaeraId = bezeroaEskaeraId;
            BezeroaEskaera = bezeroaEskaera;
            ProductoId = productoId;
            Producto = producto;
            Data = data;
        }

    }
}
