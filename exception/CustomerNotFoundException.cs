using System;

namespace ecommercemainapplication.exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(int customerId)
            : base($"Customer with ID {customerId} not found.")
        {
        }
    }
}
