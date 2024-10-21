using System;

namespace ecommercemainapplication.exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int productId)
            : base($"Product with ID {productId} not found.")
        {
        }
    }
}
