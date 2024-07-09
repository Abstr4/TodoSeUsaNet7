using Microsoft.EntityFrameworkCore;
using TodoSeUsaNet7.Models.Data;
using TodoSeUsaNet7.Models.Seeding;

namespace TodoSeUsaNet7.Models.Services
{
    public class DatabaseResetService
    {
        private readonly TodoSeUsaNet7Context _context;

        public DatabaseResetService(TodoSeUsaNet7Context context)
        {
            _context = context;
        }

        public async Task ResetDatabaseAsync()
        {
            // Clear previous data
            _context.Products.RemoveRange(_context.Products);
            _context.Bills.RemoveRange(_context.Bills);
            _context.Clients.RemoveRange(_context.Clients);
            await _context.SaveChangesAsync();

            // Reset identity columns
            await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Product', RESEED, 0);");
            await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Bill', RESEED, 0);");
            await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Client', RESEED, 0);");

            // Reseed data
            await DataSeeder.SeedDataAsync(_context);
            await _context.SaveChangesAsync();
        }
    }

}
