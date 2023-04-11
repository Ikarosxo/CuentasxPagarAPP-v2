using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CuentasxPagarAPP_v2.Context;
using CuentasxPagarAPP_v2.Models;

namespace CuentasxPagarAPP_v2.Controllers
{
    public class ConceptosController : Controller
    {
        private readonly AppDbContext _context;

        public ConceptosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Conceptos
        public async Task<IActionResult> Index(String searchString)
        {
            var conceptos = from c in _context.Conceptos
                            select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                conceptos = conceptos.Where(c => c.Descripcion.Contains(searchString)
                                              || c.Estado.Contains(searchString));
            }

            return View(await conceptos.ToListAsync());
        }

        // GET: Conceptos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Conceptos == null)
            {
                return NotFound();
            }

            var concepto = await _context.Conceptos
                .FirstOrDefaultAsync(m => m.IdConcepto == id);
            if (concepto == null)
            {
                return NotFound();
            }

            return View(concepto);
        }

        // GET: Conceptos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conceptos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConcepto,Descripcion,Estado")] Concepto concepto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concepto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concepto);
        }

        // GET: Conceptos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Conceptos == null)
            {
                return NotFound();
            }

            var concepto = await _context.Conceptos.FindAsync(id);
            if (concepto == null)
            {
                return NotFound();
            }
            return View(concepto);
        }

        // POST: Conceptos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConcepto,Descripcion,Estado")] Concepto concepto)
        {
            if (id != concepto.IdConcepto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concepto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConceptoExists(concepto.IdConcepto))
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
            return View(concepto);
        }

        // GET: Conceptos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Conceptos == null)
            {
                return NotFound();
            }

            var concepto = await _context.Conceptos
                .FirstOrDefaultAsync(m => m.IdConcepto == id);
            if (concepto == null)
            {
                return NotFound();
            }

            return View(concepto);
        }

        // POST: Conceptos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Conceptos == null)
            {
                return Problem("Entity set 'AppDbContext.Conceptos'  is null.");
            }
            var concepto = await _context.Conceptos.FindAsync(id);
            if (concepto != null)
            {
                _context.Conceptos.Remove(concepto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConceptoExists(int id)
        {
          return (_context.Conceptos?.Any(e => e.IdConcepto == id)).GetValueOrDefault();
        }
    }
}
