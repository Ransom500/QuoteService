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
    public class QuotesController : Controller
    {
        private readonly QuoteServiceDbContext _context;

        public QuotesController(QuoteServiceDbContext context)
        {
            _context = context;
        }

        // GET: Quotes
        public async Task<IActionResult> Index()
        {
            var quoteServiceDbContext = _context.Quotes.Include(q => q.Inquirer);
            return View(await quoteServiceDbContext.ToListAsync());
        }

        // GET: Quotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotes = await _context.Quotes
                .Include(q => q.Inquirer)
                .FirstOrDefaultAsync(m => m.QuoteId == id);
            if (quotes == null)
            {
                return NotFound();
            }

            return View(quotes);
        }

        // GET: Quotes/Create
        public IActionResult Create()
        {
            ViewData["InquirerId"] = new SelectList(_context.Inquirers, "InquirerId", "EmailAddress");
            return View();
        }

        // POST: Quotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuoteId,InquirerId,QuoteEstimate,ExpirationDate,DateCreated")] Quotes quotes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quotes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InquirerId"] = new SelectList(_context.Inquirers, "InquirerId", "EmailAddress", quotes.InquirerId);
            return View(quotes);
        }

        // GET: Quotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotes = await _context.Quotes.FindAsync(id);
            if (quotes == null)
            {
                return NotFound();
            }
            ViewData["InquirerId"] = new SelectList(_context.Inquirers, "InquirerId", "EmailAddress", quotes.InquirerId);
            return View(quotes);
        }

        // POST: Quotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuoteId,InquirerId,QuoteEstimate,ExpirationDate,DateCreated")] Quotes quotes)
        {
            if (id != quotes.QuoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quotes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuotesExists(quotes.QuoteId))
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
            ViewData["InquirerId"] = new SelectList(_context.Inquirers, "InquirerId", "EmailAddress", quotes.InquirerId);
            return View(quotes);
        }

        // GET: Quotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotes = await _context.Quotes
                .Include(q => q.Inquirer)
                .FirstOrDefaultAsync(m => m.QuoteId == id);
            if (quotes == null)
            {
                return NotFound();
            }

            return View(quotes);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quotes = await _context.Quotes.FindAsync(id);
            _context.Quotes.Remove(quotes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuotesExists(int id)
        {
            return _context.Quotes.Any(e => e.QuoteId == id);
        }
    }
}
