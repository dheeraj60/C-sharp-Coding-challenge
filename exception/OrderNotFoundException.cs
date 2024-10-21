using System;

namespace ecommercemainapplication.exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(int orderId)
            : base($"Order with ID {orderId} not found.")
        {
        }
    }
}
