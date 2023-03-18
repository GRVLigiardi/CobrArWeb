using System;
using CobrArWeb.Data;
using CobrArWeb.Helpers;
using CobrArWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CobrArWeb.Extensions;

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
            // Récupérez le panier de la session
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (cart == null || cart.Count == 0)
            {
                TempData["ErrorMessage"] = "Votre panier est vide.";
                return RedirectToAction("Panier");
            }

            // Commencez une transaction
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Parcourez les articles du panier et mettez à jour la quantité du produit dans la base de données
                    foreach (Item item in cart)
                    {
                        Product product = _context.Products.SingleOrDefault(p => p.Id == item.Product.Id);

                        if (product == null)
                        {
                            TempData["ErrorMessage"] = $"Le produit avec l'ID {item.Product.Id} n'a pas été trouvé.";
                            return RedirectToAction("Panier");
                        }

                        if (product.Quantite < item.Quantite)
                        {
                            TempData["ErrorMessage"] = $"Il n'y a pas assez de stock pour le produit {product.Produit}.";
                            return RedirectToAction("Panier");
                        }

                        // Mettez à jour la quantité du produit
                        product.Quantite -= item.Quantite;

                        // Enregistrez la vente
                        Ventes vente = new Ventes
                        {
                            Date = DateTime.Now,
                            ProductId = item.Product.Id,
                  
                            Quantity = item.Quantite
                        };
                        _context.Ventes.Add(vente);
                    }

                    // Enregistrez les modifications et validez la transaction
                    _context.SaveChanges();
                    transaction.Commit();

                    // Videz le panier et mettez à jour la session
                    cart.Clear();
                    HttpContext.Session.Set("cart", cart);

                    TempData["SuccessMessage"] = "Votre commande a été passée avec succès.";
                }
                catch (Exception ex)
                {
                    // Annulez la transaction en cas d'erreur
                    transaction.Rollback();

                    TempData["ErrorMessage"] = $"Une erreur s'est produite lors de la validation de votre commande: {ex.Message}";
                }
            }

            return RedirectToAction("Panier");
        }

        [Route("caisse")]
        public IActionResult Caisse()
        {
            var ventes = _context.Ventes.Include(v => v.Product).OrderByDescending(v => v.Date).ToList();
            return View(ventes);
        }

    }
}