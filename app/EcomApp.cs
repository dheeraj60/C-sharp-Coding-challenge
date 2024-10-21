using System;
using System.Collections.Generic;
using ecommercemainapplication.dao;
using ecommercemainapplication.entity;
using ecommercemainapplication.exceptions; // Ensure this namespace exists and is correct
using ecommercemainapplication.util;

namespace ecommercemainapplication.app
{
    public class EcomApp
    {
        private static string connectionString = PropertyUtil.GetPropertyString("connectionString");
        private static OrderProcessorRepository orderProcessor = new OrderProcessorRepositoryImpl(connectionString);

        public static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Register Customer");
                Console.WriteLine("2. Create Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. Add to cart");
                Console.WriteLine("5. View cart");
                Console.WriteLine("6. Place order");
                Console.WriteLine("7. View Customer Order");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Register Customer Logic
                        break;
                    case "2":
                        // Create Product Logic
                        break;
                    case "3":
                        // Delete Product Logic
                        break;
                    case "4":
                        // Add to Cart Logic
                        break;
                    case "5":
                        // View Cart Logic
                        break;
                    case "6":
                        // Place Order Logic
                        break;
                    case "7":
                        // View Customer Order Logic
                        break;
                    case "8":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }
    }
}
