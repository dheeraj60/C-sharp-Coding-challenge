namespace ecommercemainapplication.entity
{
    public class Customer
    {
        private int customerId;
        private string name;
        private string email;
        private string password;

        public Customer()
        {
            name = string.Empty;
            email = string.Empty;
            password = string.Empty;
        }

        public Customer(int customerId, string name, string email, string password)
        {
            this.customerId = customerId;
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.email = email ?? throw new ArgumentNullException(nameof(email));
            this.password = password ?? throw new ArgumentNullException(nameof(password));
        }

        // Getters and Setters
        public int CustomerId { get => customerId; set => customerId = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
    }
}
