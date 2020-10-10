using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuoteService.Models;

namespace QuoteService.Controllers
{
    public class InquirersController : Controller
    {
        private readonly QuoteServiceDbContext _context;

        public InquirersController(QuoteServiceDbContext context)
        {
            _context = context;
        }

        // GET: Inquirers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inquirers.ToListAsync());
        }

        // GET: Inquirers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inquirers = await _context.Inquirers
                .FirstOrDefaultAsync(m => m.InquirerId == id);
            if (inquirers == null)
            {
                return NotFound();
            }

            return View(inquirers);
        }

        // GET: Inquirers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inquirers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InquirerId,FirstName,LastName,PhoneNumber,EmailAddress,DateCreated")] Inquirers inquirers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inquirers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inquirers);
        }

        // GET: Inquirers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inquirers = await _context.Inquirers.FindAsync(id);
            if (inquirers == null)
            {
                return NotFound();
            }
            return View(inquirers);
        }

        // POST: Inquirers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InquirerId,FirstName,LastName,PhoneNumber,EmailAddress,DateCreated")] Inquirers inquirers)
        {
            if (id != inquirers.InquirerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inquirers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InquirersExists(inquirers.InquirerId))
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
            return View(inquirers);
        }

        // GET: Inquirers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inquirers = await _context.Inquirers
                .FirstOrDefaultAsync(m => m.InquirerId == id);
            if (inquirers == null)
            {
                return NotFound();
            }

            return View(inquirers);
        }

        // POST: Inquirers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inquirers = await _context.Inquirers.FindAsync(id);
            _context.Inquirers.Remove(inquirers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InquirersExists(int id)
        {
            return _context.Inquirers.Any(e => e.InquirerId == id);
        }
    }
}
