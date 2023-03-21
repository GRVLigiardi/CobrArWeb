using CobrArWeb.Data;

using Microsoft.AspNetCore.Mvc;


using CobrArWeb.Models;
using CobrArWeb.Models.Stock;

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
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

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
