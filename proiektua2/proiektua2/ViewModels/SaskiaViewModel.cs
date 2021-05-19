using proiektua2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.ViewModels
{
    public class SaskiaViewModel
    {
        public IList<SaskiaAlea> SaskiaAleak { get; set; }
        public decimal SaskiaGuztira { get; set; }
    }
}
