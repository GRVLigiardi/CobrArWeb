using System;
using CobrArWeb.Data;
using CobrArWeb.Helpers;
using CobrArWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;


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
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") ?? new List<Item>();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Prix * item.Quantite);
            return View("Panier");
        }

        [Route("panier")]
        public IActionResult Panier()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") ?? new List<Item>();
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
                else 
                {
                    cart.RemoveAt(index);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
            }
            return RedirectToAction("Panier");
        }
        public ActionResult MyAction()
        {
            ViewBag.cart = new List<Item>(); 
            return View();
        }

        [Route("checkout")]
        public IActionResult Checkout()
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart == null || cart.Count == 0)
            {
                return RedirectToAction("Panier");
            }

            // Begin a transaction
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Loop through cart items and update the product quantity in the database
                    foreach (Item item in cart)
                    {
                        Product product = _context.Products.FirstOrDefault(p => p.Id == item.Product.Id);
                        if (product != null)
                        {
                            product.Quantite -= item.Quantite; // Decrease the quantity
                            _context.Entry(product).State = EntityState.Modified; // Mark the product as modified
                        }
                    }

                    // Save the changes and commit the transaction
                    _context.SaveChanges();
                    transaction.Commit();

                    // Clear the cart session after a successful checkout
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);
                    TempData["SuccessMessage"] = "Le panier a été validé avec succès.";
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of any error
                    transaction.Rollback();
                    TempData["ErrorMessage"] = "Une erreur est survenue lors de la validation du panier. Veuillez réessayer.";
                }
            }

            return RedirectToAction("Panier");
        }
    }
}