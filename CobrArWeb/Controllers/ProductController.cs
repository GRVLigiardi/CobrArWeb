using Microsoft.AspNetCore.Mvc;
using CobrArWeb.Data;
using CobrArWeb.Services.Interfaces;
using CobrArWeb.Models.RechercheArbo;

namespace CobrArWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly CobrArWebContext _context;

        public ProductController(ILogger<ProductController> logger, IProductService productService, CobrArWebContext context)
        {
            _logger = logger;
            _productService = productService;
            _context = context;
        }

        public IActionResult List()
        {
            var isAuthenticated = HttpContext.Session.GetString("IsAuthenticated");
            if (isAuthenticated != null)
            {
                ViewData["IsAuthenticated"] = true;

                // Charger toutes les équipes de la base de données
                var equipes = _context.Products
                    .GroupBy(p => p.Equipe)
                    .Select(g => new EquipeViewModel
                    {
                        Equipe = g.Key,
                        Categorie = g.ToList().GroupBy(p => p.Categorie)
                            .Select(gc => new CategoryViewModel
                            {
                                Categorie = gc.Key,
                                SousCategorie = SousCategorieViewModel.Clean(gc.ToList(), new EquipeViewModel { Equipe = g.Key }),
                            }).ToList(),
                    }).ToList();

                return View(equipes);
            }
            {
                var product = new Product();
                ViewBag.products = product.ToString();
                return RedirectToAction("Index", "Home");
            }

            
        }


        // Ajouter bouton pour ajouter au panier
        public IActionResult ProductsBySubcategory(string subcategory)
        {
            var products = _context.Products.Where(p => p.SousCategorie == subcategory).ToList();
            return View(products);
        }
    }
}

