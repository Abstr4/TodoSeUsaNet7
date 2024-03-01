namespace TodoSeUsa.Models.ViewModels
{
    public class BillClientViewModel
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public int ClientId { get; set; }
        public BillClientViewModel(string firstName, string lastName, int clientId)
        {
            FirstName = firstName;
            LastName = lastName;
            ClientId = clientId;
        }

    }
}
