using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService bookService;
        private readonly IAuthService authService;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(ILogger<HomeController> logger,
                              IBookService bookService,
                              IAuthService authService,
                              UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            this.bookService = bookService;
            this.authService = authService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var booksList = bookService.GetBooks().ToList();
            return View(booksList);
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                bookService.AddBook(book);
                return RedirectToAction("Index", "Home");
            }
            return View(book);
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id)
        {
       
            return View();
        }

            public IActionResult Book( int id )
        {

            if (User.Identity.IsAuthenticated)
            {
                var name = User.Identity.Name;
                //var userId =HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier).Value;
                //ClaimsPrincipal currentUser = this.User;
                //var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

                var userid = userManager.GetUserId(User);

                //var iduser = authService.GetUserId(name);
                var reservation = new Reservation
                {
                    IdBook = id,
                   // IdUser = authService.GetUserId(name),
                    //IdUser = User.Identity.IsAuthenticated,
                    IdUser = userid,
                    ReservationDate = DateTime.Now
                };

                bookService.Booking(reservation);
                
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
