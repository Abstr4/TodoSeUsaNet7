using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoSeUsa.Models.ViewModels;
using TodoSeUsaNet7.Models;
using TodoSeUsaNet7.Models.Data;

namespace TodoSeUsa.Controllers
{
    [Authorize]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class ClientsController : Controller
    {
        private readonly TodoSeUsaNet7Context _context;

        public ClientsController(TodoSeUsaNet7Context context)
        {
            _context = context;
        }

        // GET: Clients
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {

            var clientsQuery = _context.Clients.AsQueryable();



            if (search != null && search != "")
            {
                clientsQuery = clientsQuery.Where(c => c.FirstName.Contains(search) || c.LastName.Contains(search) || c.ClientId.ToString() == search);
            }
            var clients = await clientsQuery.ToListAsync();
            return clients != null ?
                        View(clients) :
                        Problem("No clients in context");
        }


        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,FirstName,LastName,PhoneNumber,Address,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.TotalProducts = 0;
                client.TotalAmountPerProducts = 0;
                client.TotalBills = 0;
                client.TotalAmountSold = 0;
                client.ProductsSold = 0;
                client.Active = true;
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,FirstName,LastName,PhoneNumber,Address,Email,Active")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit));
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SwitchActive(int id)
        {
            Client? client = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    client.Active = !client.Active;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", new { id });
            }
            return RedirectToAction("Edit", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> ClientProducts(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Clients  is null.");
            }
            var findClient = await _context.Clients.FindAsync(id);

            if (findClient != null)
            {
                ClientProductViewModel clientProductoViewModel = new(findClient.FirstName, findClient.LastName, id);

                var clientProducts = from product in _context.Products
                                     join bill in _context.Bills on product.BillId equals bill.BillId
                                     join client in _context.Clients on bill.ClientId equals client.ClientId
                                     where client.ClientId == id
                                     select product;

                clientProductoViewModel.Products = await clientProducts.Include(p => p.Bill).Include(p => p.Bill.Client).ToListAsync();
                return View(clientProductoViewModel);

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ClientBills(int id, int? billId)
        {
            if (_context.Clients == null)
            {
                return Problem("Clients  is null.");
            }
            var findClient = await _context.Clients.FindAsync(id);

            if (findClient != null)
            {
                ClientBillViewModel clientBillViewModel = new(findClient.FirstName, findClient.LastName)
                {
                    ClientId = id
                };
                var query = from bill in _context.Bills
                               join client in _context.Clients on bill.ClientId equals client.ClientId
                               where client.ClientId == id
                               select bill;
                clientBillViewModel.Bills = await query.ToListAsync();
                if (billId != null && billId > 0)
                {
                    clientBillViewModel.Bills = clientBillViewModel.Bills.Where(f => f.BillId == billId).ToList();

                }
                return View(clientBillViewModel);

            }
            return View();
        }

        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}
