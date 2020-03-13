﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using icecreamshop.Data;
using Microsoft.AspNetCore.Identity;

namespace icecreamshop.Models
{
    public class OrderBoxesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; //Tillägg för att kunna komma åt userid

        public OrderBoxesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;//Tillägg för att komma åt userId
        }

        // GET: OrderBoxes
        [HttpGet("order")] //Override default med annan sökväg/route
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderBoxes.Include(o => o.Flavour);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderBoxes Successpage
        [HttpGet("order/success")] //sida som man hamnar på om man lyckats med beställning
        public IActionResult OrderSuccess()
        {
            return View();
        }


        // GET: OrderBoxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderBox = await _context.OrderBoxes
                .Include(o => o.Flavour)
                .FirstOrDefaultAsync(m => m.OrderBoxId == id);
            if (orderBox == null)
            {
                return NotFound();
            }

            return View(orderBox);
        }

        // GET: OrderBoxes/Create
        public IActionResult Create(int? id)
        {
            // var ChoosenObj = (_context.Flavour.Where(p => p.FlavourId == id), "FlavourId", "FlavourName");
            //ViewBag.FlavourId = ChoosenObj;
            ViewData["FlavourId"] = new SelectList(_context.Flavour.Where(p => p.FlavourId == id), "FlavourId", "FlavourName");//Sortering för att bara ta med album med det id som skickats i parameterpassning
            ViewData["UserId"] = _userManager.GetUserId(User);//För att komma åt UserId till beställning
            return View();
        }

        // POST: OrderBoxes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderBoxId,OrderSum,OrderDate,FlavourId,UserId")] OrderBox orderBox)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderBox);
                await _context.SaveChangesAsync();
                return RedirectToAction("success", "order");
                // return RedirectToAction(nameof(Index));
            }
            ViewData["FlavourId"] = new SelectList(_context.Flavour, "FlavourId", "FlavourDescription", orderBox.FlavourId);
            return View(orderBox);
        }

        // GET: OrderBoxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderBox = await _context.OrderBoxes.FindAsync(id);
            if (orderBox == null)
            {
                return NotFound();
            }
            ViewData["FlavourId"] = new SelectList(_context.Flavour, "FlavourId", "FlavourDescription", orderBox.FlavourId);
            return View(orderBox);
        }

        // POST: OrderBoxes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderBoxId,OrderSum,OrderDate,FlavourId")] OrderBox orderBox)
        {
            if (id != orderBox.OrderBoxId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderBox);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderBoxExists(orderBox.OrderBoxId))
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
            ViewData["FlavourId"] = new SelectList(_context.Flavour, "FlavourId", "FlavourDescription", orderBox.FlavourId);
            return View(orderBox);
        }

        // GET: OrderBoxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderBox = await _context.OrderBoxes
                .Include(o => o.Flavour)
                .FirstOrDefaultAsync(m => m.OrderBoxId == id);
            if (orderBox == null)
            {
                return NotFound();
            }

            return View(orderBox);
        }

        // POST: OrderBoxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderBox = await _context.OrderBoxes.FindAsync(id);
            _context.OrderBoxes.Remove(orderBox);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderBoxExists(int id)
        {
            return _context.OrderBoxes.Any(e => e.OrderBoxId == id);
        }
    }
}
