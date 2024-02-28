using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoSeUsa.Data;
using TodoSeUsa.Models.Models;

namespace TodoSeUsa.Controllers
{
    [Authorize(Roles = "Admin")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class ProductosController : Controller
    {
        private readonly TodoSeUsaContext _context;

        public ProductosController(TodoSeUsaContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index(int? id)
        {
            var todoSeUsaContext = _context.Productos.AsQueryable();
            if (id != null && id > 0)
            {
                todoSeUsaContext = todoSeUsaContext.Where(p => p.ProductoId == id);
            }
            todoSeUsaContext = todoSeUsaContext.Include(p => p.Factura);

            return View(await todoSeUsaContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Factura)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }


        // GET: Productos/Create
        [Authorize(Roles = "Admin")]
        [HttpGet("Productos/Create/{id?}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Create(int? id)
        {
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", id);
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,Tipo,Descripcion,Estado,Precio,Reacondicionado,CostoReacondicionamiento,Devolver,Devuelto,Sold,FacturaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                /*                if (producto.Reacondicionado)
                                {

                                }*/
                producto.Active = true;
                await _context.Productos.AddAsync(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", producto.FacturaId);
            return View(producto);
        }

        // GET: Productos/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,Tipo,Descripcion,Estado,Precio,Reacondicionado,CostoReacondicionamiento,Devolver,Devuelto,FacturaId")] Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProductoId))
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
            return View(producto);
        }

        // GET: Productos/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Factura)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'TodoSeUsaContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                producto.Active = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.ProductoId == id)).GetValueOrDefault();
        }
    }
}
