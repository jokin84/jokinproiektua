using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Models
{
    public class pasahitzaAldatu
    {
        //[Required]
        //[StringLength(10, ErrorMessage = "Name is too long.")]
        [Key]
        public int erabiltzeId { get; set; }
        public string PaswordOld { get; set; }
        public string Pasword1{ get; set; }
        public string Pasword2 { get; set; }

        public pasahitzaAldatu()
        {
        
        }
    }
}
