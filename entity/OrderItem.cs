﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommercemainapplication.entity
{
    public class OrderItem
    {
        private int orderItemId;
        private int orderId;
        private int productId;
        private int quantity;

        public OrderItem() { }

        public OrderItem(int orderItemId, int orderId, int productId, int quantity)
        {
            this.orderItemId = orderItemId;
            this.orderId = orderId;
            this.productId = productId;
            this.quantity = quantity;
        }

        // Getters and Setters
        public int OrderItemId { get => orderItemId; set => orderItemId = value; }
        public int OrderId { get => orderId; set => orderId = value; }
        public int ProductId { get => productId; set => productId = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}

