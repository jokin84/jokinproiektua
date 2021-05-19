using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Models
{
    public class RegisterModel
    {
        public RegisterModel() { }

        public RegisterModel(String usuario, int idusuario, String password, bool RememberMe)
        {
            this.us = usuario;
            this.idusuario = idusuario;
            this.password = password;
            this.RememberMe = RememberMe;

        }
        public String us{ get; set; }
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
