using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using icecreamshop.Models;
using icecreamshop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace icecreamshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; //Tillägg för att kunna komma åt userid

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;//Tillägg för att komma åt userId
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("om")] //Override default med annan sökväg/route  
        public IActionResult About() //Om oss sidan
        {
            return View();
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
        [HttpGet("min-profil")] //Override default med annan sökväg/route
        public async Task<IActionResult> Profile()
        {
            var userOrders = _context.OrderBoxes.Include(o => o.Flavour).Where(r => r.UserId.Contains(_userManager.GetUserId(User)))
                            .Include(t => t.User)
                            .Include(t => t.Flavour); //Hämtar ut dom beställningar inloggad user gjort
    
            return View(await userOrders.ToListAsync());
        }
    }
}
