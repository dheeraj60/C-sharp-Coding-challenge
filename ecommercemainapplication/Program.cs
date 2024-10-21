using System;
using System.Collections.Generic;
using ecommercemainapplication.dao;
using ecommercemainapplication.entity;
using ecommercemainapplication.exceptions;
using ecommercemainapplication.util;

namespace ecommercemainapplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = PropertyUtil.GetPropertyString("DefaultConnection");
            using (var repository = new OrderProcessorRepositoryImpl(connectionString))
            {
                while (true)
                {
                    Console.WriteLine("\tECOMMERCE APPLICATION");
                    Console.WriteLine("<--------------------------------------------->");
                    Console.WriteLine("\t");
                    Console.WriteLine("\t1. Register Customer");
                    Console.WriteLine("\t");
                    Console.WriteLine("\t2. Create Product");
                    Console.WriteLine("\t");
                    Console.WriteLine("\t3. Delete Product");
                    Console.WriteLine("\t");
                    Console.WriteLine("\t4. Add to Cart");
                    Console.WriteLine("\t");
                    Console.WriteLine("\t5. View Cart");
                    Console.WriteLine("\t");
                    Console.WriteLine("\t6. Place Order");
                    Console.WriteLine("\t");
                    Console.WriteLine("\t7. View Customer Order");
                    Console.WriteLine("\t");
                    Console.WriteLine("\t8. Exit");
                    Console.WriteLine("\t");
                    Console.Write("\tSelect an option: ");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            RegisterCustomer(repository);
                            break;
                        case "2":
                            CreateProduct(repository);
                            break;
                        case "3":
                            DeleteProduct(repository);
                            break;
                        case "4":
                            AddToCart(repository);
                            break;
                        case "5":
                            ViewCart(repository);
                            break;
                        case "6":
                            PlaceOrder(repository);
                            break;
                        case "7":
                            ViewCustomerOrder(repository);
                            break;
                        case "8":
                            return;
                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                }
            }
        }

        static void RegisterCustomer(OrderProcessorRepositoryImpl repository)
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine() ?? string.Empty; 
            Console.Write("Enter email: ");
            string email = Console.ReadLine() ?? string.Empty; 
            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? string.Empty; 

            Customer customer = new Customer { Name = name, Email = email, Password = password };

            try
            {
                if (repository.CreateCustomer(customer))
                {
                    Console.WriteLine("Customer registered successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void CreateProduct(OrderProcessorRepositoryImpl repository)
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine() ?? string.Empty; 
            Console.Write("Enter price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine() ?? "0"); 
            Console.Write("Enter description: ");
            string description = Console.ReadLine() ?? string.Empty; 
            Console.Write("Enter stock quantity: ");
            int stockQuantity = Convert.ToInt32(Console.ReadLine() ?? "0"); 
            Product product = new Product { Name = name, Price = price, Description = description, StockQuantity = stockQuantity };

            try
            {
                if (repository.CreateProduct(product))
                {
                    Console.WriteLine("Product created successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void DeleteProduct(OrderProcessorRepositoryImpl repository)
        {
            Console.Write("Enter product ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int productId)) 
            {
                try
                {
                    if (repository.DeleteProduct(productId))
                    {
                        Console.WriteLine("Product deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid product ID.");
            }
        }

        static void AddToCart(OrderProcessorRepositoryImpl repository)
        {
            Console.Write("Enter customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerId) && customerId > 0) 
            {
                Console.Write("Enter product ID: ");
                if (int.TryParse(Console.ReadLine(), out int productId) && productId > 0) 
                {
                    Console.Write("Enter quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0) 
                    {
                        Customer customer = new Customer { CustomerId = customerId };
                        Product product = new Product { ProductId = productId };

                        try
                        {
                            if (repository.AddToCart(customer, product, quantity))
                            {
                                Console.WriteLine("Product added to cart successfully.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid product ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid customer ID.");
            }
        }

        static void ViewCart(OrderProcessorRepositoryImpl repository)
        {
            Console.Write("Enter customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerId) && customerId > 0)
            {
                Customer customer = new Customer { CustomerId = customerId };

                try
                {
                    var products = repository.GetAllFromCart(customer);
                    Console.WriteLine("Products in cart:");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price}, Description: {product.Description}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid customer ID.");
            }
        }

        static void PlaceOrder(OrderProcessorRepositoryImpl repository)
        {
            Console.Write("Enter customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerId) && customerId > 0)
            {
                Console.Write("Enter shipping address: ");
                string shippingAddress = Console.ReadLine() ?? string.Empty;

                Customer customer = new Customer { CustomerId = customerId };
                List<KeyValuePair<Product, int>> orderItems = new List<KeyValuePair<Product, int>>();

                var productsInCart = repository.GetAllFromCart(customer);
                foreach (var product in productsInCart)
                {
                    Console.Write($"Enter quantity for {product.Name}: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0) 
                    {
                        orderItems.Add(new KeyValuePair<Product, int>(product, quantity));
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity. Skipping product.");
                    }
                }

                try
                {
                    if (repository.PlaceOrder(customer, orderItems, shippingAddress))
                    {
                        Console.WriteLine("Order placed successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid customer ID.");
            }
        }

        static void ViewCustomerOrder(OrderProcessorRepositoryImpl repository)
        {
            Console.Write("Enter customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerId) && customerId > 0)
            {
                try
                {
                    var orders = repository.GetOrdersByCustomer(customerId);
                    Console.WriteLine("Orders for customer:");
                    foreach (var order in orders)
                    {
                        Console.WriteLine($"Product ID: {order.Key.ProductId}, Name: {order.Key.Name}, Quantity: {order.Value}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid customer ID.");
            }
        }
    }
}
