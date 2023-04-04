using CobrArWeb.Data;
using CobrArWeb.Models;
using CobrArWeb.Models.Chat;
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
        private readonly CobrArWebContext _context;
        private string _currentUserEmail;

        public HomeController(ILogger<HomeController> logger, CobrArWebContext context,
            IAuthenticationService authenticationService)
        {
            _context = context;
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

                // Get the current user by email
                var currentUser = _context.Users.FirstOrDefault(u => u.Email == isAuthenticated);
                if (currentUser != null)
                {
                    // Set the user name in ViewBag
                    ViewBag.UserName = currentUser.Email; // Replace 'Email' with the user name property if you have one
                    _currentUserEmail = currentUser.Email; // Store the current user email
                }
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

        public IActionResult GetMessages()
        {
            var messages = _context.Messages.Select(m => new
            {
                Id = m.Id,
                UserName = m.UserName,
                Content = m.Content,
                Timestamp = m.Timestamp
            }).ToList();

            return Json(messages);
        }

        [HttpPost]
        public IActionResult SendMessage(MessageViewModel messageViewModel)
        {
            if (ModelState.IsValid)
            {
                var message = new Message
                {
                    UserName = messageViewModel.UserName,
                    Content = messageViewModel.Content,
                    Timestamp = DateTime.Now
                };

                _context.Messages.Add(message);
                _context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
    }
}