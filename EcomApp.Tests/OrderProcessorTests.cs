using NUnit.Framework;
using ecommercemainapplication.dao;
using ecommercemainapplication.entity;

namespace ecommercemainapplication.Tests
{
    public class OrderProcessorTests
    {
        private OrderProcessorRepositoryImpl orderProcessor;
        private const string connectionString = "Server=LAPTOP-E8Q2MF96\\SQLEXPRESS;Database=ecommercedatabase;Trusted_Connection=True;"; // Replace with your actual connection string

        [SetUp]
        public void Setup()
        {
            orderProcessor = new OrderProcessorRepositoryImpl(connectionString);
        }

        [Test]
        public void Test_ProductCreation_Success()
        {
            var product = new Product
            {
                ProductId = 1,
                Name = "Test Product",
                Price = 10.0m
            };

            bool result = orderProcessor.CreateProduct(product);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_AddToCart_Success()
        {
            var customer = new Customer { CustomerId = 1 }; 
            var product = new Product { ProductId = 1 }; 
            int quantity = 1;

            bool result = orderProcessor.AddToCart(customer, product, quantity);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_PlaceOrder_Success()
        {
            var customer = new Customer { CustomerId = 1 };
            var orderItems = new List<KeyValuePair<Product, int>>
            {
                new KeyValuePair<Product, int>(new Product { ProductId = 1 }, 1) 
            };
            string shippingAddress = "123 Test St, Test City";

            bool result = orderProcessor.PlaceOrder(customer, orderItems, shippingAddress);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_CreateCustomer_Success()
        {
            Customer customer = new Customer { Name = "christoper john", Email = "johoe@example.com", Password = "securpasword" };

            bool result = orderProcessor?.CreateCustomer(customer) ?? false;

            Assert.That(result, Is.True, "Expected customer creation to succeed.");
        }


        [TearDown]
        public void TearDown()
        {
            orderProcessor.Dispose();
        }
    }
}



