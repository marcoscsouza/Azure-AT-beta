using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web
{
    public class MalucoController : Controller
    {
        private readonly webContext _context;

        public MalucoController(webContext context)
        {
            _context = context;
        }

        // GET: Maluco
        public async Task<IActionResult> Index()
        {
            return View(await _context.Maluco.ToListAsync());
        }

        // GET: Maluco/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maluco = await _context.Maluco
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maluco == null)
            {
                return NotFound();
            }

            return View(maluco);
        }

        // GET: Maluco/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maluco/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,UserId")] Maluco maluco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maluco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maluco);
        }

        // GET: Maluco/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maluco = await _context.Maluco.FindAsync(id);
            if (maluco == null)
            {
                return NotFound();
            }
            return View(maluco);
        }

        // POST: Maluco/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,UserId")] Maluco maluco)
        {
            if (id != maluco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maluco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MalucoExists(maluco.Id))
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
            return View(maluco);
        }

        // GET: Maluco/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maluco = await _context.Maluco
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maluco == null)
            {
                return NotFound();
            }

            return View(maluco);
        }

        // POST: Maluco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maluco = await _context.Maluco.FindAsync(id);
            _context.Maluco.Remove(maluco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MalucoExists(int id)
        {
            return _context.Maluco.Any(e => e.Id == id);
        }
    }
}
