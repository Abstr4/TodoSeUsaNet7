using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoSeUsaNet7.Models;
using TodoSeUsaNet7.Models.Data;


namespace TodoSeUsa.Controllers
{
    [Authorize]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly TodoSeUsaNet7Context _context;

        public ProductsController(TodoSeUsaNet7Context context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int? id)
        {
            var todoSeUsaContext = _context.Products.AsQueryable();
            if (id != null && id > 0)
            {
                todoSeUsaContext = todoSeUsaContext.Where(p => p.ProductId == id);
            }
            todoSeUsaContext = todoSeUsaContext.Include(p => p.Bill).Include(p => p.Bill.Client);

            return View(await todoSeUsaContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Bill)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // GET: Products/Create

        /*[HttpGet("Products/Create/{id}")]*/
        [HttpGet("Products/Create")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Create(int id, int? clientId)
        {
            if(clientId !=  null)
            {
                var clientBillsContext = _context.Bills.Where(bill => bill.ClientId == clientId);
                var clientSelectList = new List<SelectListItem>();
                foreach (Bill bill in clientBillsContext)
                {
                    clientSelectList.Add(new SelectListItem { Text = $"Factura Nro. {bill.BillId} del {bill.DateCreated.ToString("dd/MM/yyyy")}", Value = $"{bill.BillId}" });
                }

                var client = _context.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId).Result;

                ViewData["BillId"] = clientSelectList;
                ViewData["Client"] = client;
                return View();
            }
            var BillIdSelectList = new List<SelectListItem>
            {
                new SelectListItem { Text = $"Factura Nro. {id}", Value = $"{id}" }
            };
            ViewData["BillId"] = BillIdSelectList;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Type,Description,State,Price,Reacondicionado,CostoReacondicionamiento,Devolver,Devuelto,Sold,BillId")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Active = true;
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (product.BillId == 0)
            {
                
                ModelState.AddModelError("BillId", "Please select a valid Bill ID.");
            }
            var BillIdSelectList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Seleccione el Código de la factura...", Value = null }
            };

            ViewData["BillId"] = BillIdSelectList;
            return View(product);
        }

        // GET: Products/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Type,Description,State,Price,Reacondicionado,CostoReacondicionamiento,Devolver,Devuelto,BillId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(product.ProductId))
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
            return View(product);
        }

        // GET: Products/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Bill)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'TodoSeUsaNet7Context.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.Active = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
