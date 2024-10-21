using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ecommercemainapplication.entity;
using ecommercemainapplication.exceptions;
using ecommercemainapplication.util;

namespace ecommercemainapplication.dao
{
    public class OrderProcessorRepositoryImpl : OrderProcessorRepository, IDisposable
    {
        private SqlConnection connection;

        public OrderProcessorRepositoryImpl(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public bool CreateProduct(Product product)
        {
            try
            {
                using (var cmd = new SqlCommand("INSERT INTO products (name, price, description, stockQuantity) VALUES (@name, @price, @description, @stockQuantity)", connection))
                {
                    cmd.Parameters.AddWithValue("@name", product.Name);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@description", product.Description);
                    cmd.Parameters.AddWithValue("@stockQuantity", product.StockQuantity);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating product: " + ex.Message);
            }
        }

        public bool CreateCustomer(Customer customer)
        {
            try
            {
                using (var cmd = new SqlCommand("INSERT INTO customers (name, email, password) VALUES (@name, @email, @password)", connection))
                {
                    cmd.Parameters.AddWithValue("@name", customer.Name);
                    cmd.Parameters.AddWithValue("@email", customer.Email);
                    cmd.Parameters.AddWithValue("@password", customer.Password);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating customer: " + ex.Message);
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                using (var cmd = new SqlCommand("DELETE FROM products WHERE product_id = @productId", connection))
                {
                    cmd.Parameters.AddWithValue("@productId", productId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting product: " + ex.Message);
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                using (var cmd = new SqlCommand("DELETE FROM customers WHERE customer_id = @customerId", connection))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting customer: " + ex.Message);
            }
        }

        public bool AddToCart(Customer customer, Product product, int quantity)
        {
            try
            {
                using (var cmd = new SqlCommand("INSERT INTO cart (customer_id, product_id, quantity) VALUES (@customerId, @productId, @quantity)", connection))
                {
                    cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@productId", product.ProductId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding to cart: " + ex.Message);
            }
        }

        public bool RemoveFromCart(Customer customer, Product product)
        {
            try
            {
                using (var cmd = new SqlCommand("DELETE FROM cart WHERE customer_id = @customerId AND product_id = @productId", connection))
                {
                    cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@productId", product.ProductId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing from cart: " + ex.Message);
            }
        }

        public List<Product> GetAllFromCart(Customer customer)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (var cmd = new SqlCommand("SELECT p.product_id, p.name, p.price, p.description FROM cart c JOIN products p ON c.product_id = p.product_id WHERE c.customer_id = @customerId", connection))
                {
                    cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                Description = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving products from cart: " + ex.Message);
            }

            return products;
        }

        public bool PlaceOrder(Customer customer, List<KeyValuePair<Product, int>> orderItems, string shippingAddress)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    decimal totalPrice = 0;

                    // Create order
                    using (var cmd = new SqlCommand("INSERT INTO orders (customer_id, order_date, total_price, shipping_address) OUTPUT INSERTED.order_id VALUES (@customerId, @orderDate, @totalPrice, @shippingAddress)", connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                        cmd.Parameters.AddWithValue("@orderDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@totalPrice", totalPrice);
                        cmd.Parameters.AddWithValue("@shippingAddress", shippingAddress);

                        int orderId = (int)cmd.ExecuteScalar();

                        // Add order items
                        foreach (var item in orderItems)
                        {
                            totalPrice += item.Key.Price * item.Value;

                            using (var cmdItem = new SqlCommand("INSERT INTO order_items (order_id, product_id, quantity) VALUES (@orderId, @productId, @quantity)", connection, transaction))
                            {
                                cmdItem.Parameters.AddWithValue("@orderId", orderId);
                                cmdItem.Parameters.AddWithValue("@productId", item.Key.ProductId);
                                cmdItem.Parameters.AddWithValue("@quantity", item.Value);
                                cmdItem.ExecuteNonQuery();
                            }
                        }

                        // Update total price in the orders table
                        cmd.Parameters["@totalPrice"].Value = totalPrice;
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error placing order: " + ex.Message);
                }
            }
        }

        public List<KeyValuePair<Product, int>> GetOrdersByCustomer(int customerId)
        {
            List<KeyValuePair<Product, int>> orders = new List<KeyValuePair<Product, int>>();
            try
            {
                using (var cmd = new SqlCommand("SELECT p.product_id, p.name, oi.quantity FROM order_items oi JOIN products p ON oi.product_id = p.product_id WHERE oi.order_id IN (SELECT order_id FROM orders WHERE customer_id = @customerId)", connection))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                ProductId = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            int quantity = reader.GetInt32(2);
                            orders.Add(new KeyValuePair<Product, int>(product, quantity));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving orders by customer: " + ex.Message);
            }

            return orders;
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}


