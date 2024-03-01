using System.ComponentModel.DataAnnotations;
using TodoSeUsaNet7.Models;

namespace TodoSeUsa.Models.ViewModels
{
    public class ClientProductViewModel
    {

        [Display(Name = "client's id")]
        public int ClientId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public List<Product>? Products { get; set; }
        public ClientProductViewModel(string firstName, string lastName, int clientId)
        {
            ClientId = clientId;
            FirstName = firstName;
            LastName = lastName;
        }

    }

}
