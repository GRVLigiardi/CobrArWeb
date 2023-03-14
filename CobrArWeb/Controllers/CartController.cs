using CobrArWeb.Data;
using CobrArWeb.Helpers;
using CobrArWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CobrArWeb.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly CobrArWebContext _context;

        public CartController(CobrArWebContext context)
        {
            _context = context;
        }

        [Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Prix * item.Quantite);
            return View("Panier");
        }

        [Route("panier")]
        public IActionResult Panier()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Prix * item.Quantite);
            return View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = product, Quantite = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(product.Id);
                if (index != -1)
                {
                    cart[index].Quantite++;
                }
                else
                {
                    cart.Add(new Item { Product = product, Quantite = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Panier");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Panier");
        }

        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        [Route("increment/{id}")]
        public IActionResult Increment(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantite++;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Panier");
        }

        [Route("decrement/{id}")]
        public IActionResult Decrement(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                if (cart[index].Quantite > 1) // vérifier si la quantité est supérieure à 1
                {
                    cart[index].Quantite--;
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                else // si la quantité est égale à 1, supprimer l'article
                {
                    cart.RemoveAt(index);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
            }
            return RedirectToAction("Panier");
        }
        public ActionResult MyAction()
        {
            ViewBag.cart = new List<Item>(); // initialiser la variable cart
            // ajouter des éléments à la liste cart ici
            return View();
        }
    }
}