using TodoSeUsaNet7.Models;

namespace TodoSeUsa.Models.ViewModels
{
    public class ClientBillViewModel
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Bill>? Bills { get; set; }
        public ClientBillViewModel(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
