using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Models
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Categoria")]
        public int Marcaid { get; set; }
        public virtual Marca Marca { get; set; }
        [Display(Name = "Categoria")]

        public int Categoriaid{ get; set; }
        public virtual Categoria Categoria { get; set; }
        public String descripcion { get; set; }
        public float precio { get; set; }

        public float precioTotal { get; set; }
        public string Nombre { get; set; }
        public String imagen { get; set; }
        public bool Eskaintza { get; internal set; }
        public int Deskontua { get; set; }

        public Producto()
        {
        }
        public Producto(int idProductos, int Marcaid, Marca MarcaIzena, int Categoriaid, Categoria CategoriaIzena, String descripcion, float precio, float precioTotal, String imagen, bool Eskaintza, int Deskontua)
        {
            this.id = idProductos;
            this.Marcaid = Marcaid;
            this.Marca = MarcaIzena;
            this.Categoriaid = Categoriaid;
            this.Categoria = CategoriaIzena;
            this.descripcion = descripcion;
            this.precio = precio;
            this.precioTotal = precioTotal;
            this.imagen = imagen;
            this.Eskaintza = Eskaintza;
            this.Deskontua = Deskontua;

        }



        public Producto(int id, string nombre, string descripcion, float precioTotal, string imagen)
        {
            this.id = id;
            Nombre = nombre;
            this.descripcion = descripcion;
            this.precioTotal = precioTotal;
            this.imagen = imagen;
        }
    }



}


