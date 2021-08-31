using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using web.Data;
using web.Models;
using webServicesDBInfra.Interfaces;

namespace web.Controllers
{
    public class MalucoController : Controller
    {
        private readonly webContext _context;
        private readonly IBlobService _blobService;

        public MalucoController(webContext context, IBlobService blobService)
        {
            _context = context;
            _blobService = blobService;
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
        public async Task<IActionResult> Create( IFormCollection form,
                                        [Bind("Id,Nome,UserId")] Maluco maluco)
        {
            if (ModelState.IsValid)
            {
                var file = form.Files.SingleOrDefault();
                var streamFile = file.OpenReadStream();
                var uriImage = await _blobService.UploadAsync(streamFile);
                maluco.ImageUri = uriImage;

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
        public async Task<IActionResult> Edit(IFormCollection form, 
                                              int id, Maluco maluco)
        {
            if (id != maluco.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(maluco);
            }
            try
            {
                var malucoDoBanco = await _context.Maluco.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                    
                var file = form.Files.SingleOrDefault();

                if (file == null)
                {
                    var manterUsuario = new Maluco();
                    manterUsuario.ImageUri = malucoDoBanco.ImageUri;
                    manterUsuario.Nome = malucoDoBanco.Nome;
                    manterUsuario.UserId = malucoDoBanco.UserId;

                }
                else
                {
                    if (malucoDoBanco.ImageUri != null )
                    {
                        await _blobService.DeleteAsync(malucoDoBanco.ImageUri);
                    }
                    
                    var streamFile = file.OpenReadStream();
                    var uriImage = await _blobService.UploadAsync(streamFile);
                    maluco.ImageUri = uriImage;

                }
                
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
        public async Task<IActionResult> DeleteConfirmed(IFormCollection form, int id)
        {
            var maluco = await _context.Maluco.FindAsync(id);

            await _blobService.DeleteAsync(maluco.ImageUri);
            
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
