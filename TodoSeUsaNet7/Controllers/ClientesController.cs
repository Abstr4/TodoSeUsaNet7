using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoSeUsa.Data;
using TodoSeUsa.Models.Models;
using TodoSeUsa.Models.ViewModels;

namespace TodoSeUsa.Controllers
{
    [Authorize(Roles = "Admin")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class ClientesController : Controller
    {
        private readonly TodoSeUsaContext _context;

        public ClientesController(TodoSeUsaContext context)
        {
            _context = context;
        }

        // GET: Clientes
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var clientsQuery = _context.Clientes.AsQueryable();

            if (search != null && search != "")
            {
                clientsQuery = clientsQuery.Where(c => c.Nombre.Contains(search) || c.Apellido.Contains(search) || c.ClienteId.ToString() == search);
            }
            var clients = await clientsQuery.ToListAsync();
            return clients != null ?
                        View(clients) :
                        Problem("Entity set 'TodoSeUsaContext.Clientes'  is null.");
        }


        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Nombre,Apellido,Telefono,Direccion,Email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nombre,Apellido,Telefono,Direccion,Email,Active")] Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteId))
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
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SwitchActive(int id)
        {
            Cliente? cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    cliente.Active = !cliente.Active;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteId))
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
        public async Task<IActionResult> ClienteProductos(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Clientes  is null.");
            }
            var findCliente = await _context.Clientes.FindAsync(id);

            if (findCliente != null)
            {
                ClienteProductoViewModel clienteProductoViewModel = new(findCliente.Nombre, findCliente.Apellido, id);

                var clientProducts = from producto in _context.Productos
                                     join factura in _context.Facturas on producto.FacturaId equals factura.FacturaId
                                     join cliente in _context.Clientes on factura.ClienteId equals cliente.ClienteId
                                     where cliente.ClienteId == id
                                     select producto;

                clienteProductoViewModel.Productos = await clientProducts.ToListAsync();
                return View(clienteProductoViewModel);

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ClienteFacturas(int id, int? facturaId)
        {
            if (_context.Clientes == null)
            {
                return Problem("Clientes  is null.");
            }
            var findCliente = await _context.Clientes.FindAsync(id);

            if (findCliente != null)
            {
                ClienteFacturaViewModel clienteFacturaViewModel = new(findCliente.Nombre, findCliente.Apellido);
                clienteFacturaViewModel.ClienteId = id;
                var consulta = from factura in _context.Facturas
                               join cliente in _context.Clientes on factura.ClienteId equals cliente.ClienteId
                               where cliente.ClienteId == id
                               select factura;
                clienteFacturaViewModel.Facturas = await consulta.ToListAsync();
                if (facturaId != null && facturaId > 0)
                {
                    clienteFacturaViewModel.Facturas = clienteFacturaViewModel.Facturas.Where(f => f.FacturaId == facturaId).ToList();

                }
                return View(clienteFacturaViewModel);

            }
            return View();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(e => e.ClienteId == id)).GetValueOrDefault();
        }
    }
}
