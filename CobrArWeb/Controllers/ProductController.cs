using Microsoft.AspNetCore.Mvc;
using CobrArWeb.Data;
using CobrArWeb.Services.Interfaces;
using CobrArWeb.Models.RechercheArbo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

                var equipes = _context.Products
                    .Include(p => p.Equipe)
                    .Include(p => p.Categorie)
                    .Include(p => p.SousCategorie)
                    .Include(p => p.Taille)
                    .Include(p => p.Fournisseur)
                    .GroupBy(p => p.Equipe.Nom)
                    .Select(g => new EquipeViewModel
                    {
                        Equipe = g.Key,
                        Categorie = g.ToList().GroupBy(p => p.Categorie.Nom).Select(gc => new CategoryViewModel { Categorie = gc.Key, SousCategorie = SousCategorieViewModel.Clean(gc.ToList(), new EquipeViewModel { Equipe = g.Key }) }).ToList(),
                    }).ToList();

                var listViewModel = new ListViewModel
                {
                    EquipeViewModelList = equipes
                };

                return View(listViewModel);
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
            var products = _context.Products.Where(p => p.SousCategorie.Nom == subcategory).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult ProductsByTeam(string teamId)
        {
            var products = _context.Products
                .Where(p => p.Equipe.Nom == teamId)
                .ToList();

            return Json(products);
        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View("Stock", products);
        }

        public IActionResult Stock()
        {
            // Initialiser le modèle (par exemple, une liste de produits)
            var products = _context.Products.ToList();

            // Passer le modèle à la vue
            return View(products);
        }
           [HttpGet]
           public IActionResult FilteredProducts(string team, string category)
           {
               var filteredProducts = _context.Products
                   .Include(p => p.Equipe)
                   .Include(p => p.Categorie)
                   .Include(p => p.SousCategorie)
                   .Include(p => p.Taille)
                   .Include(p => p.Fournisseur)
                   .Where(p => p.Equipe.Nom == team && p.Categorie.Nom == category)
                   .ToList();

               return PartialView("ProductListPartialList", filteredProducts);
           }
    }
}

        
