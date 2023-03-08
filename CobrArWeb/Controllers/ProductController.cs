using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CobrArWeb.Data;
using CobrArWeb.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CobrArWeb.Models;

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

                // Charger toutes les catégories et sous-catégories associées de la base de données
                var categories = _context.Categories
                    .Include(c => c.SousCategories)
                    .ToList();

                // Créer une arborescence de catégories et sous-catégories pour afficher les produits
                var categoryTree = new List<CategoryViewModel>();
                foreach (var category in categories)
                {
                    var categoryViewModel = new CategoryViewModel
                    {
                        Name = category.Name,
                        Subcategories = new List<SubcategoryViewModel>()
                    };

                    foreach (var subcategory in category.SousCategories)
                    {
                        var subcategoryViewModel = new SubcategoryViewModel
                        {
                            Name = subcategory.Name,
                            Products = _context.Products
                                .Where(p => p.SousCategorie == subcategory.Name)
                                .ToList()
                        };

                        if (subcategoryViewModel.Products.Any())
                        {
                            categoryViewModel.Subcategories.Add(subcategoryViewModel);
                        }
                    }

                    if (categoryViewModel.Subcategories.Any())
                    {
                        categoryTree.Add(categoryViewModel);
                    }
                }

                return View(categoryTree);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public IActionResult ProductsBySubcategory(string subcategory)
        {
            var products = _context.Products.Where(p => p.SousCategorie == subcategory).ToList();
            return View(products);
        }
    }
}

