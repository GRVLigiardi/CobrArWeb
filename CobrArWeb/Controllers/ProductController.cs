using Microsoft.AspNetCore.Mvc;
using CobrArWeb.Data;
using CobrArWeb.Services;
using CobrArWeb.Services.Interfaces;
using CobrArWeb.Services.Interfaces;

namespace CobrArWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, CobrArWebContext context,
            IProductService productService)
        {
            _productService = productService;
            _logger = logger;
        }

        public IActionResult List()
        {
            var isAuthenticated = HttpContext.Session.GetString("IsAuthenticated");
            if (isAuthenticated != null)
            {
                ViewData["IsAuthenticated"] = true;
                var products = _productService.GetProducts();
                return View(products);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}