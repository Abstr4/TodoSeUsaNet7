using TodoSeUsaNet7.Models;

namespace TodoSeUsa.Models.ViewModels
{
    public class BillProductViewModel
    {
        public Bill Bill { get; set; }
        public List<Product> Products { get; set; }
        public BillProductViewModel(Bill bill, List<Product> products)
        {
            Products = products;
            Bill = bill;
        }

    }
}
