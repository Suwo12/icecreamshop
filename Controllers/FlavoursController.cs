using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using icecreamshop.Data;
using icecreamshop.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace icecreamshop.Models
{
    public class FlavoursController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnviroment;//tillägg för IHostingEnviroment
        public FlavoursController(ApplicationDbContext context, IHostingEnvironment hostingEnviroment)
        {
            _context = context;
           this.hostingEnviroment = hostingEnviroment;//Deklarerar hostingEnviroment
        }

        // GET: Flavours
        /* [HttpGet("smaker")] //Override default med annan sökväg/route
          public async Task<IActionResult> Index()
          {
              return View(await _context.Flavour.ToListAsync());
          }*/

        //GET MED SÖK NY START
        // GET: Flavours
        [HttpGet("smaker")] //Override default med annan sökväg/route
        public async Task<IActionResult> Index(string searchString)//strängen skickas med som parameter i uri från startup cs routing
        {
            //queryn har definierats här men inte körts mot databasen
            var flavours = from a in _context.Flavour select a;

            //Om strängen inte är tom ska filtrering ske smak-namn
            if (!String.IsNullOrEmpty(searchString))
            {
                flavours = flavours.Where(s => s.FlavourName.Contains(searchString)); //Lambda Expression i LINQ query
            }

            return View(await flavours.ToListAsync());
        }
        //GET NY SLUT


        // GET: Flavours/Details/5
        [HttpGet("smaker/detaljer/")] //Override default med annan sökväg/route
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flavour = await _context.Flavour
                .FirstOrDefaultAsync(m => m.FlavourId == id);
            if (flavour == null)
            {
                return NotFound();
            }

            return View(flavour);
        }

        // GET: Flavours/Create
      [HttpGet("smaker/skapa")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flavours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        [HttpPost("smaker/skapa")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(FlavourCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null; //Filnamn sätts till null initialt
                if (model.Photo != null)
                {
                  string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");//Sparar ner wwwroot path och lagrar i sträng
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;//Skapar unika filnamn för att inte overrida bilder med guid global unique identifier, varje gång GUID metoden anropas returneras ett unikt id som konverteras till sträng med tillägg för ett _ och sedan läggs uploadedFilename till
                   string filePath = Path.Combine(uploadsFolder, uniqueFileName);//Kombinerar strängarna uploadsFolder och uniqueFileName och lagrar det i en strängvariabel
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));                                                          //Kopierar uppladdad bild till image folder med metoden CopyTo i IFormFile 
                }
                Flavour newFlavour = new Flavour
                {
                    FlavourName = model.FlavourName,
                    FlavourDescription = model.FlavourDescription,
                    FlavourPrice = model.FlavourPrice,
                    PhotoPath = uniqueFileName
                };

                _context.Add(newFlavour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        // GET: Flavours/Edit/5
        [HttpGet("smaker/redigera/")]
        [Authorize(Policy = "RequireAdmin")]//Kräver att man är Admin med using Microsoft.AspNetCore.Authorization;
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flavour = await _context.Flavour.FindAsync(id);
            if (flavour == null)
            {
                return NotFound();
            }
            return View(flavour);
        }

        // POST: Flavours/Edit/5
       
        [Authorize(Policy = "RequireAdmin")]//Kräver att man är Admin med using Microsoft.AspNetCore.Authorization;
        [HttpPost("smaker/redigera/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlavourId,FlavourName,FlavourDescription, FlavourPrice")] Flavour flavour)
        {
            if (id != flavour.FlavourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flavour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlavourExists(flavour.FlavourId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flavour);
        }

        // GET: Flavours/Delete/5
        [Authorize(Policy = "RequireAdmin")]//Kräver att man är Admin med using Microsoft.AspNetCore.Authorization;
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flavour = await _context.Flavour
                .FirstOrDefaultAsync(m => m.FlavourId == id);
            if (flavour == null)
            {
                return NotFound();
            }

            return View(flavour);
        }

        // POST: Flavours/Delete/5
        [Authorize(Policy = "RequireAdmin")]//Kräver att man är Admin med using Microsoft.AspNetCore.Authorization;
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flavour = await _context.Flavour.FindAsync(id);
            _context.Flavour.Remove(flavour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlavourExists(int id)
        {
            return _context.Flavour.Any(e => e.FlavourId == id);
        }
    }
}
