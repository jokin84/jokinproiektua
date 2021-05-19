using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proiektua2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiektua2.Models
{
    public class Saskia
    {
        public string SaskiaId { get; set; }
        public const string CartSessionKey = "cartId";

        public static Saskia SaskiaLortu()
        {
            var cart = new Saskia();
            cart.SaskiaId = cart.SaskiaLortuId();
            return cart;
        }
        public static Saskia SaskiaLortu(Controller controller)
        {
            return SaskiaLortu();
        }
        public string SaskiaLortuId(HttpContext contestua)
        {
            if (contestua.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(contestua.User.Identity.Name))
                {
                    contestua.Session.SetString(CartSessionKey, contestua.User.Identity.Name);
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    contestua.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return contestua.Session.GetString(CartSessionKey);
        }
        ///nuevo metodo
        public string SaskiaLortuId()
        {
            string id;
            if(CartSessionKey == "cartId") { 
            if (AdminDbContext.sesion == true)
            {
                Guid tempCartId = Guid.NewGuid();
                id = tempCartId.ToString();

            }
            else
            {
                id = CartSessionKey;
            }
            }
            return SaskiaController.tempCartId.ToString();
        }
    }

}
