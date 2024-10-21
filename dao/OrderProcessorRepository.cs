using System.Collections.Generic;
using ecommercemainapplication.entity;

namespace ecommercemainapplication.dao
{
    public interface OrderProcessorRepository
    {
        bool CreateProduct(Product product);
        bool CreateCustomer(Customer customer);
        bool DeleteProduct(int productId);
        bool DeleteCustomer(int customerId);
        bool AddToCart(Customer customer, Product product, int quantity);
        bool RemoveFromCart(Customer customer, Product product);
        List<Product> GetAllFromCart(Customer customer);
        bool PlaceOrder(Customer customer, List<KeyValuePair<Product, int>> productsWithQuantities, string shippingAddress);
        List<KeyValuePair<Product, int>> GetOrdersByCustomer(int customerId);
    }
}
