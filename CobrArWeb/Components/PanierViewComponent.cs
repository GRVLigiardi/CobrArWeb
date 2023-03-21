using CobrArWeb.Data;
using CobrArWeb.Helpers;
using CobrArWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CobrArWeb.Components
{
    public class PanierViewComponent : ViewComponent
    {
        private readonly CobrArWebContext _context;

        public PanierViewComponent(CobrArWebContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") ?? new List<Item>();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Prix * item.Quantite);
            return View("_PanierPartial");
        }
    }
}
