using CobrArWeb.Data;
using CobrArWeb.Models.Stock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CobrArWeb.Models.Stock;
using CobrArWeb.Models;

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

        // GET: StockController
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var viewModel = new StockViewModel
            {
                NewProduct = new Product(),
                Products = products
            };
            return View("Stock", viewModel);
        }

        public IActionResult AddProduct()
        {
            var equipeList = _context.Equipes.Select(e => e.Nom).ToList();
            var categorieList = _context.Categories.Select(c => c.Nom).ToList();
            var fournisseurList = _context.Fournisseurs.Select(f => f.Nom).ToList();
            var viewModel = new AddProductViewModel();

            // Récupérer toutes les équipes
            viewModel.Equipes = (IEnumerable<string>)_context.Equipes.ToList();

            // Récupérer toutes les catégories
            viewModel.Categories = (IEnumerable<string>)_context.Categories.ToList();

            // Récupérer tous les fournisseurs
            viewModel.Fournisseurs = (IEnumerable<string>)_context.Fournisseurs.ToList();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }
    }
}
