using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Models
{
    public class Usuario
    {
        public Usuario() { }

        public Usuario(String usuario, int idusuario, String password,bool RememberMe) {
            this.usuario = usuario;
            this.idusuario = idusuario;
            this.password = password;
            this.RememberMe=RememberMe;

        }
        public String usuario { get; set; }
        [Key]
        public int idusuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String password { get; set; }
        public virtual String tipo { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
