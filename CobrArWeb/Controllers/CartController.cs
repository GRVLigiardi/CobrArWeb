using System;
using CobrArWeb.Data;
using CobrArWeb.Helpers;
using CobrArWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CobrArWeb.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var modesDePaiement = _context.MDPs.ToList();
            ViewBag.ModesDePaiement = modesDePaiement;

            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") ?? new List<Item>();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Prix * item.Quantite);

            return View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            Product product = _context.Products
                .Include(p => p.Equipe)
                .Include(p => p.Categorie)
                .Include(p => p.SousCategorie)
                .Include(p => p.Taille)
                .Include(p => p.Fournisseur)
                .FirstOrDefault(p => p.Id == id);

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
            return RedirectToAction("Panier", "Cart");
        }


        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Panier", "Cart");
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
            return RedirectToAction("Panier", "Cart");
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
            return RedirectToAction("Panier", "Cart");
        }
        public ActionResult MyAction()
        {
            ViewBag.cart = new List<Item>(); 
            return View();
        }

        [Route("checkout")]
        public IActionResult Checkout(int modeDePaiementId, int? modeDePaiementId2 = null)
        {

            // Récupérez le panier de la session
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (cart == null || cart.Count == 0)
            {
                TempData["ErrorMessage"] = "Votre panier est vide.";
                return RedirectToAction("Panier");
            }
            decimal totalPrice = (decimal)cart.Sum(item => item.Product.Prix * item.Quantite);
            ViewBag.total = totalPrice;

            decimal ajustementPrix = 0;
            decimal ajustementPrix2 = 0;
            MDP mdp2 = null;
            switch (modeDePaiementId)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    string pourcentage = Request.Form["pourcentage"];
                    decimal pourcentageValue;
                    if (decimal.TryParse(pourcentage, out pourcentageValue))
                    {
                        ajustementPrix = (ViewBag.total * pourcentageValue) / 100;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Veuillez entrer un pourcentage valide.";
                        return RedirectToAction("Panier");
                    }
                    break;
                                case 4:
                    string montantCarteCadeau = Request.Form["montantCarteCadeau"];

                    if (string.IsNullOrEmpty(montantCarteCadeau))
                    {
                        TempData["ErrorMessage"] = "Veuillez entrer un montant pour la carte cadeau.";
                        return RedirectToAction("Panier");
                    }

                    decimal montantCarteCadeauValue;
                    if (decimal.TryParse(montantCarteCadeau, out montantCarteCadeauValue))
                    {
                        if (montantCarteCadeauValue >= ViewBag.total)
                        {
                            decimal difference = montantCarteCadeauValue - ViewBag.total;
                            TempData["SuccessMessage"] = $"Il reste {difference} $ sur la carte cadeau.";
                            return RedirectToAction("Panier");
                        }
                        else
                        {
                            HttpContext.Session.SetString("carteCadeauValue", montantCarteCadeauValue.ToString());
                            TempData["WarningMessage"] = $"Il reste {ViewBag.total - montantCarteCadeauValue} $ à payer. Choisissez un deuxième moyen de paiement.";
                            if (modeDePaiementId2.HasValue)
                            {
                                mdp2 = _context.MDPs.FirstOrDefault(m => m.Id == modeDePaiementId2.Value);
                                if (mdp2 == null)
                                {
                                    TempData["ErrorMessage"] = "Deuxième mode de paiement non valide.";
                                    return RedirectToAction("Panier");
                                }
                                decimal restantAPayer = ViewBag.total - montantCarteCadeauValue;
                                switch (modeDePaiementId2.Value)
                                {
                                    case 1:
                                        ajustementPrix2 = -restantAPayer;
                                        break;
                                    case 2:
                                        ajustementPrix2 = -restantAPayer;
                                        break;
                                    case 3:
                                        string pourcentage2 = Request.Form["pourcentage2"];
                                        decimal pourcentageValue2;
                                        if (decimal.TryParse(pourcentage2, out pourcentageValue2))
                                        {
                                            ajustementPrix2 = ((ViewBag.total - montantCarteCadeauValue) * pourcentageValue2) / 100;
                                        }
                                        else
                                        {
                                            TempData["ErrorMessage"] = "Veuillez entrer un pourcentage valide pour le deuxième mode de paiement.";
                                            return RedirectToAction("Panier");
                                        }
                                        break;
                                   
                                    case 6:
                                        string justificationGratuitSuiteKdo = Request.Form["justificationGratuit"];
                                        if (!string.IsNullOrEmpty(justificationGratuitSuiteKdo))
                                        {
                                            ajustementPrix = -ViewBag.total;
                                            TempData["SuccessMessage"] = $"La commande a été effectuée gratuitement avec la justification suivante : GRATUIT : {justificationGratuitSuiteKdo}";
                                        }
                                        else
                                        {
                                            TempData["ErrorMessage"] = "Veuillez entrer une justification pour la commande gratuite.";
                                            return RedirectToAction("Panier");
                                        }
                                        break;
                                    default:
                                        TempData["ErrorMessage"] = "Mode de paiement non valide.";
                                        return RedirectToAction("Panier");
                                }
                            }
                            else
                            {
                                return RedirectToAction("Panier");
                            }
                        }
                    }

                        break;
              
                case 6:
                    string justificationGratuit = Request.Form["justificationGratuit"];
                    if (!string.IsNullOrEmpty(justificationGratuit))
                    {
                        ajustementPrix = -ViewBag.total;
                        TempData["SuccessMessage"] = $"La commande a été effectuée gratuitement avec la justification suivante : GRATUIT : {justificationGratuit}";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Veuillez entrer une justification pour la commande gratuite.";
                        return RedirectToAction("Panier");
                    }
                    break;
                default:
                    TempData["ErrorMessage"] = "Mode de paiement non valide.";
                    return RedirectToAction("Panier");
            }

            decimal adjustedTotal = totalPrice + ajustementPrix + ajustementPrix2;
            ViewBag.adjustedTotal = adjustedTotal;
            ViewBag.ajustementPrix = ajustementPrix;
            // Récupére le MDP à partir de la base de données en utilisant modeDePaiementId
            MDP mdp = _context.MDPs.FirstOrDefault(m => m.Id == modeDePaiementId);

            if (mdp == null)
            {
                TempData["ErrorMessage"] = "Mode de paiement non valide.";
                return RedirectToAction("Panier");
            }

            // Commence une transaction
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Parcoure les articles du panier et mettez à jour la quantité du produit dans la base de données
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

                        // Mette à jour la quantité du produit
                        product.Quantite -= item.Quantite;

                        // Enregistre la vente
                        Ventes vente = new Ventes
                        {
                            Date = DateTime.Now,
                            ProductId = item.Product.Id,
                            Produit = item.Product.Produit,
                            Categorie = item.Product.Categorie.Nom,
                            SousCategorie = item.Product.SousCategorie.Nom,
                            Equipe = item.Product.Equipe.Nom,
                            Taille = item.Product.Taille.Nom,
                            Quantite = item.Product.Quantite,
                            Prix = item.Product.Prix,
                            Fournisseur = item.Product.Fournisseur.Nom,
                            Quantity = item.Quantite,
                            MDPNom = mdp.Nom,
                            MDPId = modeDePaiementId,
                            MDPId2 = mdp2?.Id, 
                            MDPNom2 = mdp2?.Nom,
                            AjustementPrix = (ajustementPrix != 0 || ajustementPrix2 != 0) ? (ajustementPrix + ajustementPrix2) : (decimal?)null,
                        };
                        _context.Ventes.Add(vente);
                    }

                    // Enregistre les modifications et validez la transaction
                    _context.SaveChanges();
                    transaction.Commit();

                    // Vide le panier et mettez à jour la session
                    cart.Clear();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart); // Utilise SetObjectAsJson

                    TempData["SuccessMessage"] = "Votre commande a été passée avec succès.";
                }
                catch (Exception ex)
                {
                    // Annule la transaction en cas d'erreur
                    transaction.Rollback();

                    TempData["ErrorMessage"] = $"Une erreur s'est produite lors de la validation de votre commande: {ex.Message}";
                }
            }

            return RedirectToAction("Panier");
        }

        [Route("caisse")]
        public IActionResult Caisse(string filterType = "day")
        {
            DateTime now = DateTime.Now;
            DateTime startDate = now;

            switch (filterType.ToLower())
            {
                case "day":
                    startDate = now.Date;
                    break;
                case "week":
                    startDate = now.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday).Date;
                    break;
                case "month":
                    startDate = new DateTime(now.Year, now.Month, 1);
                    break;
                default:
                    startDate = DateTime.MinValue;
                    break;
            }

            var ventes = _context.Ventes
                .Include(v => v.Product)
                .Include(v => v.MDP)
                .Where(v => v.Date >= startDate)
                .OrderByDescending(v => v.Date)
                .ToList();
            return View(ventes);
        }

        [Route("continue")]
        public IActionResult ContinueShopping()
        {
            return RedirectToAction("List", "Product");
        }

        [HttpPost]
        public IActionResult ProcessExchange(int equipe, int categorie, int sousCategorie, int productId)
        {
            var equipeObj = _context.Equipes.FirstOrDefault(e => e.Id == equipe);
            var categorieObj = _context.Categories.FirstOrDefault(c => c.Id == categorie);
            var sousCategorieObj = _context.SousCategories.FirstOrDefault(sc => sc.Id == sousCategorie);

            return RedirectToAction("Checkout");
        }

    }
}