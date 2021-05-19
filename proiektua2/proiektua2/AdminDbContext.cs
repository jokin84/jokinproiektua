using Microsoft.EntityFrameworkCore;
using proiektua2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2
{
    public class AdminDbContext : DbContext
    {

        private readonly DbContextOptions _options;

        public AdminDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }
        public AdminDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public static bool sesion;

        public static Usuario user = new Usuario();
        public DbSet<Producto> Productos { get; set; }
         public DbSet<Usuario> Usuarios { get; set; }


         public DbSet<Marca> Marcas { get; set; }

         public DbSet<Pedido> Pedidos { get; set; }

         public DbSet<Linea> Linea { get; set; }

         public DbSet<Categoria> Categoria { get; set; }
        public DbSet<SaskiaAlea> SaskiaAlea { get; set; }
        public DbSet<Saskia> Saskia { get; set; }
        public DbSet<BezeroaEskaera> BezeroaEskaera { get; set; }
        public DbSet<Erosketa> Erosketa { get; set; }
        public DbSet<pasahitzaAldatu> PasaitzaAldatu { get; set; }

    }
}

