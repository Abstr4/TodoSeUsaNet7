using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoSeUsaNet7.Models.Data;
using TodoSeUsaNet7.Models.Seeding;

namespace TodoSeUsaNet7.Controllers
{


    [Authorize]
    public class SeedController : ControllerBase
    {
        private readonly TodoSeUsaNet7Context _dbContext;

        public SeedController(TodoSeUsaNet7Context dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> SeedData()
        {
            await DataSeeder.SeedDataAsync(_dbContext);
            return Ok("Data seeding completed.");
        }
    }
}
