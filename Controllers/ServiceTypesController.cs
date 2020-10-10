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
    public class ServiceTypesController : Controller
    {
        private readonly QuoteServiceDbContext _context;

        public ServiceTypesController(QuoteServiceDbContext context)
        {
            _context = context;
        }

        // GET: ServiceTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceTypes.ToListAsync());
        }

        // GET: ServiceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceTypes = await _context.ServiceTypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (serviceTypes == null)
            {
                return NotFound();
            }

            return View(serviceTypes);
        }

        // GET: ServiceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,Title,DateCreated")] ServiceTypes serviceTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceTypes);
        }

        // GET: ServiceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceTypes = await _context.ServiceTypes.FindAsync(id);
            if (serviceTypes == null)
            {
                return NotFound();
            }
            return View(serviceTypes);
        }

        // POST: ServiceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeId,Title,DateCreated")] ServiceTypes serviceTypes)
        {
            if (id != serviceTypes.TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceTypesExists(serviceTypes.TypeId))
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
            return View(serviceTypes);
        }

        // GET: ServiceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceTypes = await _context.ServiceTypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (serviceTypes == null)
            {
                return NotFound();
            }

            return View(serviceTypes);
        }

        // POST: ServiceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceTypes = await _context.ServiceTypes.FindAsync(id);
            _context.ServiceTypes.Remove(serviceTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceTypesExists(int id)
        {
            return _context.ServiceTypes.Any(e => e.TypeId == id);
        }
    }
}
