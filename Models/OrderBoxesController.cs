using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using icecreamshop.Data;

namespace icecreamshop.Models
{
    public class OrderBoxesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderBoxesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderBoxes
        [HttpGet("order")] //Override default med annan sökväg/route
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderBoxes.Include(o => o.Flavour);
            return View(await applicationDbContext.ToListAsync());
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
            ViewData["FlavourId"] = new SelectList(_context.Flavour, "FlavourId", "FlavourDescription");
            return View();
        }

        // POST: OrderBoxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderBoxId,OrderSum,OrderDate,FlavourId")] OrderBox orderBox)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderBox);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
