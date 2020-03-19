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
using System.Net.Mail;//Tillägg för mail
using System.Net;//Tillägg för mail
using SendGrid;//Tillägg för mail ny
using SendGrid.Helpers.Mail;//Tillägg för mail ny
using Microsoft.Extensions.Configuration;//Tillägg för mail ny

namespace icecreamshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; //Tillägg för att kunna komma åt userid
        private readonly IConfiguration _configuration; //Tillägg för mail
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;//Tillägg för att komma åt userId
            _configuration = configuration;//Tillägg för mail
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

        // Tillägg för mailfunktion via App Send Grid //
        [HttpGet("kontakt")] //Override default med annan sökväg/route
        public ActionResult SendEmail()
        {
            return View();
        }

        //Tillägg för att ta emot post req från SendEmail form
        [HttpPost("kontakt")]//Override default med annan sökväg/route
       public async Task<ActionResult> SendEmail(string subject, string message)//Skickar med parameterpassning för input sträng ämne och meddelande
            {
                var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("glassigkontakt@gmail.com", "Sender"),//Mail som avsändare
                    Subject = subject, //Ämne
                    PlainTextContent = message,//Meddelande
                };
                msg.AddTo(new EmailAddress("glassigkontakt@gmail.com", "Receiver"));//Mottagande mail Ändra här Lars och Mattias om man vill testa mail-funktionen
            ViewBag.Success = "Vi på glassigt tackar för ditt mail!";//Meddelde för att visa att mail kom fram skickas med viewbag till sidan
            return View(await client.SendEmailAsync(msg));
            }

        }

}
