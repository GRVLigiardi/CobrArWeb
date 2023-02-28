using CobrArWeb.Data;
using CobrArWeb.Models;
using CobrArWeb.Models.Views.Common;
using CobrArWeb.Models.Views.Home;
using CobrArWeb.Services;
using CobrArWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CobrArWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public HomeController(ILogger<HomeController> logger, CobrArWebContext context,
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        public IActionResult Login(LoginVM loginVM) 
        {
            bool isAutheticated = _authenticationService.AuthenticateUser(loginVM.Email, loginVM.Password);
            if (isAutheticated)
            {
                HttpContext.Session.SetString("IsAuthenticated", loginVM.Email);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", new ErrorVM() { ErrorTag = "ERR_LOGIN", ErrorText = "Invalid login or password" });
            }
        }

        public IActionResult CreateUser(LoginVM loginVM)
        {
            if (loginVM == null || string.IsNullOrWhiteSpace(loginVM.Email))
            {
                return View();
            }
            else
            {
                _authenticationService.CreateUser(loginVM.Email, loginVM.Password);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Index(ErrorVM errorVM)
        {
            var isAuthenticated = HttpContext.Session.GetString("IsAuthenticated");
            if (isAuthenticated != null)
            {
                ViewData["IsAuthenticated"] = true;
            }
            else
            {
                ViewData["IsAuthenticated"] = false;
            }

            return View(errorVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}