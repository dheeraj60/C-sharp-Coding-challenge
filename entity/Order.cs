using System;

namespace ecommercemainapplication.entity
{
    public class Order
    {
        private int orderId;
        private int customerId;
        private DateTime orderDate;
        private decimal totalPrice;
        private string shippingAddress;

        public Order()
        {
            shippingAddress = string.Empty; 
        }

        public Order(int orderId, int customerId, DateTime orderDate, decimal totalPrice, string shippingAddress)
        {
            this.orderId = orderId;
            this.customerId = customerId;
            this.orderDate = orderDate;
            this.totalPrice = totalPrice;
            this.shippingAddress = shippingAddress ?? throw new ArgumentNullException(nameof(shippingAddress));
        }

        // Getters and Setters
        public int OrderId { get => orderId; set => orderId = value; }
        public int CustomerId { get => customerId; set => customerId = value; }
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public decimal TotalPrice { get => totalPrice; set => totalPrice = value; }
        public string ShippingAddress { get => shippingAddress; set => shippingAddress = value; }
    }
}
