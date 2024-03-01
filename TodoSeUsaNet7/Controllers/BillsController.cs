using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoSeUsa.Models.ViewModels;
using TodoSeUsaNet7.Models;
using TodoSeUsaNet7.Models.Data;


namespace TodoSeUsa.Controllers
{
    [Authorize]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class BillsController : Controller
    {
        private readonly TodoSeUsaNet7Context _context;
        public BillsController(TodoSeUsaNet7Context context)
        {
            _context = context;
        }



        // GET: Bills
        [HttpGet]
        public async Task<IActionResult> Index(int? search)
        {

            var billQuery = _context.Bills.AsQueryable();
            if (search != null && search > 0)
            {
                billQuery = billQuery.Where(f => f.BillId== search);
            }
            var todoSeUsaContext = billQuery.Include(f => f.Client);
            return View(await _context.Bills.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bills == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(f => f.Client)
                .FirstOrDefaultAsync(m => m.BillId== id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create

        [HttpGet("Bills/Create/{id?}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Create(int? id)
        {
            if (id != null && id > 0)
            {
                Client? client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == id);
                if (client != null)
                {
                    BillClientViewModel billClient = new(client.FirstName, client.LastName, id.Value);
                    return View(billClient);
                }
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", id);
            return View();
        }

        // POST: Bills/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBill([Bind("BillId, ClientId")] Bill bill)
        {
            bill.DateCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                await _context.AddAsync(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", bill.ClientId);
            return View(bill);
        }

        // GET: Bills/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bills == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "LastName", bill.ClientId);
            return View(bill);
        }

        // POST: Bills/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillId,DateCreated,Total,ClientId")] Bill bill)
        {
            if (id != bill.BillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(bill.BillId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        RedirectToAction(nameof(Index));
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "LastName", bill.ClientId);
            return View(bill);
        }

        // GET: Bills/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bills == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(f => f.Client)
                .FirstOrDefaultAsync(m => m.BillId== id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bills == null)
            {
                return Problem("Entity set 'TodoSeUsaNet7Context.Bills'  is null.");
            }
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null)
            {
                bill.Active = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Bills/BillProducts/5
        public async Task<IActionResult> BillProducts(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bill = await _context.Bills.Where(f => f.BillId== id).FirstOrDefaultAsync();
            var products = await _context.Products.Where(p => p.BillId== id).ToListAsync();
            if (products == null || bill == null)
            {
                return NotFound();
            }
            BillProductViewModel billProductViewModel = new(bill, products);
            return View(billProductViewModel);
        }

        private bool FacturaExists(int id)
        {
            return (_context.Bills?.Any(e => e.BillId== id)).GetValueOrDefault();
        }


    }
}
