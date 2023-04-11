using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CuentasxPagarAPP_v2.Context;
using CuentasxPagarAPP_v2.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CuentasxPagarAPP_v2.Controllers
{
    public class DocumentosxPagarController : Controller
    {
        private readonly AppDbContext _context;

        public DocumentosxPagarController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DocumentosxPagar
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DocumentosxPagar.Include(d => d.Proveedor);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DocumentosxPagar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DocumentosxPagar == null)
            {
                return NotFound();
            }

            var documentoxPagar = await _context.DocumentosxPagar
                .Include(d => d.Proveedor)
                .FirstOrDefaultAsync(m => m.NumDocument == id);
            if (documentoxPagar == null)
            {
                return NotFound();
            }

            return View(documentoxPagar);
        }

        // GET: DocumentosxPagar/Create
        public IActionResult Create()
        {
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "Nombre");
            return View();
        }

        // POST: DocumentosxPagar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumDocument,NumFacturaPagar,FechaDocumento,Monto,FechaRegistro,IdProveedor,Estado")] DocumentoxPagar documentoxPagar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentoxPagar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "Nombre", documentoxPagar.IdProveedor);
            return View(documentoxPagar);
        }

        // GET: DocumentosxPagar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DocumentosxPagar == null)
            {
                return NotFound();
            }

            var documentoxPagar = await _context.DocumentosxPagar.FindAsync(id);
            if (documentoxPagar == null)
            {
                return NotFound();
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "Nombre", documentoxPagar.IdProveedor);
            return View(documentoxPagar);
        }

        // POST: DocumentosxPagar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumDocument,NumFacturaPagar,FechaDocumento,Monto,FechaRegistro,IdProveedor,Estado")] DocumentoxPagar documentoxPagar)
        {
            if (id != documentoxPagar.NumDocument)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentoxPagar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentoxPagarExists(documentoxPagar.NumDocument))
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
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "Nombre", documentoxPagar.IdProveedor);
            return View(documentoxPagar);
        }

        // GET: DocumentosxPagar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DocumentosxPagar == null)
            {
                return NotFound();
            }

            var documentoxPagar = await _context.DocumentosxPagar
                .Include(d => d.Proveedor)
                .FirstOrDefaultAsync(m => m.NumDocument == id);
            if (documentoxPagar == null)
            {
                return NotFound();
            }

            return View(documentoxPagar);
        }

        // POST: DocumentosxPagar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DocumentosxPagar == null)
            {
                return Problem("Entity set 'AppDbContext.DocumentosxPagar'  is null.");
            }
            var documentoxPagar = await _context.DocumentosxPagar.FindAsync(id);
            if (documentoxPagar != null)
            {
                _context.DocumentosxPagar.Remove(documentoxPagar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentoxPagarExists(int id)
        {
          return (_context.DocumentosxPagar?.Any(e => e.NumDocument == id)).GetValueOrDefault();
        }

        // POST: DocumentosxPagar/Contabilizar/5
        [HttpPost]
        public async Task<IActionResult> Contabilizar(int id)
        {
            // Buscar el documento correspondiente al id
            var documentoxPagar = await _context.DocumentosxPagar.FindAsync(id);
            if (documentoxPagar == null)
            {
                return NotFound();
            }

                // Generar el asiento contable
            string nombreAux = "CuentaxPagar " + documentoxPagar.IdProveedor;
            string origen = documentoxPagar.Estado == "Pendiente" ? "CR" : "DB";
            decimal monto = documentoxPagar.Monto;

            var asientoContable = new
            {
                id_aux = 6,
                nombre_aux = nombreAux,
                cuenta = 2,
                origen = origen,
                monto = monto
            };

            // Convertir el asiento contable a formato JSON
            var asientoContableJSON = JsonConvert.SerializeObject(asientoContable);

            // Crear un objeto HttpClient para realizar la petición HTTP
            using (var httpClient = new HttpClient())
            {
                // Establecer la URL del servicio web donde se enviará el asiento contable
                httpClient.BaseAddress = new Uri("http://localhost:3000/contable");

                // Configurar el encabezado de la petición HTTP
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Crear el contenido de la petición HTTP con el asiento contable en formato JSON
                var content = new StringContent(asientoContableJSON, Encoding.UTF8, "application/json");

                // Enviar la petición HTTP al servicio web
                var response = await httpClient.PostAsync(httpClient.BaseAddress, content);

                // Leer el código de estado de la respuesta HTTP
                int statusCode = (int)response.StatusCode;

                // Mostrar un mensaje emergente con el código de estado de la respuesta HTTP
                TempData["StatusMessage"] = $"HTTP Status: {statusCode}";

                // Redirigir al usuario de vuelta a la lista de documentos
                return RedirectToAction(nameof(Index));
            }
        }
    }

}
