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

                var teams = _context.Teams
                    .Include(t => t.Categories)
                        .ThenInclude(c => c.SousCategories)
                            .ThenInclude(s => s.Products)
                    .ToList();

                return View(teams);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
