using proiektua2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Services
{
    public interface ISaskiaService
    {
        void SaskiaGehitu(int ardoaId, string saskiaId);
        IList<SaskiaAlea> SaskiaLortuAleak(string saskiaId);
        decimal Guztira(string saskiaId);

        SaskiaAlea SaskiaAleaDatuak(int ardoaId, string saskiaId);

        int SaskiaKendu(int ardoaId, string saskiaId);

        int ItemGuztiak(string saskiaId);
        void EskaeraBezeroaGehitu(BezeroaEskaera bezeroaEskaera);
        void EskaeraSortu(BezeroaEskaera bezeroaEskaera, string SaskiaId);
        void SaskiaEzabatu(string saskiaId);
        BezeroaEskaera BezeroaDatuak(int erabiltzailea);
        bool BezeroaKonprobatu(int erabiltzailea);
        void BezeroaAldatu(BezeroaEskaera bezeroaEskaera);
        int ErosketaGehitu(BezeroaEskaera bezeroaEskaera, string saskiaId);

    }

}

