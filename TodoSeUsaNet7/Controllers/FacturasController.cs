using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoSeUsa.Data;
using TodoSeUsa.Models.Models;
using TodoSeUsa.Models.ViewModels;

namespace TodoSeUsa.Controllers
{
    [Authorize(Roles = "Admin")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class FacturasController : Controller
    {
        private readonly TodoSeUsaContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public FacturasController(TodoSeUsaContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        // GET: Facturas
        [HttpGet]
        public async Task<IActionResult> Index(int? search)
        {

            var facturaQuery = _context.Facturas.AsQueryable();
            if (search != null && search > 0)
            {
                facturaQuery = facturaQuery.Where(f => f.FacturaId == search);
            }
            var todoSeUsaContext = facturaQuery.Include(f => f.Cliente);
            return View(await _context.Facturas.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        [Authorize(Roles = "Admin")]
        [HttpGet("Facturas/Create/{id?}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Create(int? id)
        {
            if (id != null && id > 0)
            {
                Cliente? client = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == id);
                if (client != null)
                {
                    FacturaClienteViewModel facturaCliente = new(client.Nombre, client.Apellido, id);
                    return View(facturaCliente);
                }
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", id);
            return View();
        }

        // POST: Facturas/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFactura([Bind("FacturaId, ClienteId")] Factura factura)
        {
            factura.FechaCreacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                await _context.AddAsync(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", factura.ClienteId);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Apellido", factura.ClienteId);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacturaId,FechaCreacion,Total,ClienteId")] Factura factura)
        {
            if (id != factura.FacturaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.FacturaId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Apellido", factura.ClienteId);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Facturas == null)
            {
                return Problem("Entity set 'TodoSeUsaContext.Facturas'  is null.");
            }
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                factura.Active = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Facturas/FacturaProductos/5
        public async Task<IActionResult> FacturaProductos(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var factura = await _context.Facturas.Where(f => f.FacturaId == id).FirstOrDefaultAsync();
            var productos = await _context.Productos.Where(p => p.FacturaId == id).ToListAsync();
            // var productos = from producto in _context.Productos where producto.FacturaId == id select producto;
            if (productos == null || factura == null)
            {
                return NotFound();
            }
            FacturaProductoViewModel facturaProductoViewModel = new(factura, productos);
            return View(facturaProductoViewModel);
        }

        private bool FacturaExists(int id)
        {
            return (_context.Facturas?.Any(e => e.FacturaId == id)).GetValueOrDefault();
        }


    }
}
