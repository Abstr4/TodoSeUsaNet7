using Bogus;
using TodoSeUsaNet7.Models.Data;

namespace TodoSeUsaNet7.Models.Seeding
{
    public class DataSeeder
    {
        public static async Task SeedDataAsync(TodoSeUsaNet7Context context)
        {
            Random random = new Random();

            var clients = new Faker<Client>()
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("##########"))
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Active, true)
                .Generate(3);

            await context.Clients.AddRangeAsync(clients);


            var bills = new List<Bill>();
            foreach (var client in clients)
            {

                var bill = new Faker<Bill>()
                    .RuleFor(b => b.DateCreated, f => f.Date.Past())
                    .Generate();
                bill.Active = true;
                bill.Closed = false;

                bills.Add(bill);

                client.Bills = new List<Bill>() { bill };
            }

            var products = new List<Product>();

            foreach (var bill in bills)
            {
                var billProducts = new Faker<Product>()
                    .RuleFor(p => p.Type, f => f.Commerce.ProductName())
                    .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                    .RuleFor(p => p.Condition, f => f.Lorem.Word())
                    .RuleFor(p => p.State, f => f.Commerce.ProductAdjective())
                    .RuleFor(p => p.Price, f => f.Random.Int(50, 500))
                    .RuleFor(p => p.Reaconditioned, f => f.Random.Bool())
                    .RuleFor(p => p.MustReturn, f => f.Random.Bool())
                    .RuleFor(p => p.Sold, f => f.Random.Bool())
                    .RuleFor(p => p.Active, true)
                    .GenerateBetween(3, 5);

                // reaconditioning cost and returned depends on reaconditioned and must return respectively to have a value
                foreach (var product in billProducts)
                {
                    if (product.Reaconditioned)
                    {
                        product.ReaconditioningCost = random.Next(10, 50);
                    }
                    if (product.MustReturn)
                    {
                        product.Returned = random.Next(2) == 0;
                    }
                }

                bill.Products = billProducts;

                products.AddRange(billProducts);
            }

            await context.Bills.AddRangeAsync(bills);

            await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();
        }
    }
}
