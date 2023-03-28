using CobrArWeb.Data;
using CobrArWeb.Models;
using CobrArWeb.Models.RechercheArbo;
using CobrArWeb.Models.Stock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CobrArWeb.Controllers
{
    public class StockController : Controller
    {
        private readonly CobrArWebContext _context;

        public Product NewProduct { get; set; } = new Product();

        public StockController(CobrArWebContext context)
        {
            _context = context;
        }

        private List<EquipeViewModel> GetEquipeViewModelList()
        {
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

            return equipes;
        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var equipes = _context.Equipes.ToList();
            var categories = _context.Categories.ToList();
            var sousCategories = _context.SousCategories.ToList();
            var tailles = _context.Tailles.ToList();
            var fournisseurs = _context.Fournisseurs.ToList();
            var viewModel = new StockViewModel
            {
                NewProduct = new Product(),
                Products = products,
                Equipes = equipes,
                Categories = categories,
                SousCategories = sousCategories,
                Tailles = tailles,
                Fournisseurs = _context.Fournisseurs.ToList(),
                EquipeViewModelList = GetEquipeViewModelList() // Ajout de cette ligne
            };

            SetViewBagDropdownLists();
            return View("Stock", viewModel);
        }

        public void SetViewBagDropdownLists(int? selectedTailleId = null, int? selectedCategorieId = null)
        {
            // Définir la liste de tailles disponibles dans le ViewBag
            ViewBag.TailleList = new SelectList(_context.Tailles, "Id", "Nom", selectedTailleId);

            // Définir la liste de catégories disponibles dans le ViewBag
            ViewBag.CategorieList = new SelectList(_context.Categories, "Id", "Nom");

            // Définir la liste de sous-catégories disponibles dans le ViewBag
            ViewBag.SousCategorieList = new SelectList(_context.SousCategories.Where(sc => sc.CategorieId == selectedCategorieId), "Id", "Nom");

            // Définir la liste d'équipes disponibles dans le ViewBag
            ViewBag.EquipeList = new SelectList(_context.Equipes, "Id", "Nom");

            // Définir la liste de fournisseurs disponibles dans le ViewBag
            ViewBag.FournisseurList = new SelectList(_context.Fournisseurs, "Id", "Nom");
        }


        [HttpGet]
        public JsonResult GetSousCategoriesByCategorie(int categorieId)
        {
            var sousCategories = _context.SousCategories
                .Where(sc => sc.CategorieId == categorieId)
                .Select(sc => new { sc.Id, sc.Nom })
                .ToList();

            Console.WriteLine($"Sous-categories for categorieId {categorieId}: {sousCategories.Count}");
            foreach (var sc in sousCategories)
            {
                Console.WriteLine($" - {sc.Id}: {sc.Nom}");
            }

            return Json(sousCategories);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Include(p => p.Categorie).Include(p => p.SousCategorie).Include(p => p.Equipe).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            SetViewBagDropdownLists(product.TailleId, product.CategorieId);
            return View("EditProduct", product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            // Vérifier si l'ID du produit existe déjà dans la base de données
            var existingProduct = _context.Products.Find(product.Id);

            if (existingProduct != null)
            {
                // Mettre à jour le produit existant dans la base de données
                _context.Entry(existingProduct).CurrentValues.SetValues(product);
            }
            else
            {
                // Ajouter le nouveau produit à la base de données
                _context.Products.Add(product);
            }

            _context.SaveChanges();

            // Rediriger vers la vue "Stock.cshtml" après l'édition
            return RedirectToAction("Index");
        }
    }
}