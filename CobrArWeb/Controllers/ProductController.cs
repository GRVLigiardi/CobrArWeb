using Microsoft.AspNetCore.Mvc;
using CobrArWeb.Data;
using CobrArWeb.Services.Interfaces;
using CobrArWeb.Models.RechercheArbo;
using CobrArWeb.Helpers;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public IActionResult ProductsByTeam(string teamId)
        {
            var products = _context.Products
                .Where(p => p.Equipe == teamId)
                .ToList();

            return Json(products);
        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View("Stock", products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Stock()
        {
            // Initialiser le modèle (par exemple, une liste de produits)
            var products = _context.Products.ToList();

            // Passer le modèle à la vue
            return View(products);
        }

    }
            }

