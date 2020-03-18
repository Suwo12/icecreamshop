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

        // Tillägg för mailfunktion //
        [HttpGet("kontakt")] //Override default med annan sökväg/route
        public ActionResult SendEmail()
        {
            return View();
        }

        //Tillägg för att ta emot post req från SendEmail form
        [HttpPost("kontakt")]//Override default med annan sökväg/route
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("glassigkontakt@gmail.com", "Glassig kund");//E-post som används som den man skickar ifrån
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "losen1234";//Lösenord för den mail man använder
                    var sub = subject;//Lagrar ämne i variabel
                    var body = message;//Lagrar meddelande i variabel
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                        ViewBag.Success = "Vi på glassigt tackar för ditt mail!";//Meddelde för att visa att mail kom fram skickas med viewbag till sidan
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ajdå något gick fel, ditt mail skickades inte";//Meddelde för att visa att mail inte kunde skickas med viewbag till sidan
            }
            return View();
        }
    }
}
