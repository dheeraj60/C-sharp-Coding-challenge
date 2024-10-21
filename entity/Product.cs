using System;

namespace ecommercemainapplication.entity
{
    public class Product
    {
        private int productId;
        private string name;
        private decimal price;
        private string description;
        private int stockQuantity;
        private int quantity; 

        public Product()
        {
            name = string.Empty;       
            description = string.Empty; 
            quantity = 0; 
        }

        public Product(int productId, string name, decimal price, string description, int stockQuantity)
        {
            this.productId = productId;
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.price = price;
            this.description = description ?? throw new ArgumentNullException(nameof(description)); 
            this.stockQuantity = stockQuantity;
            this.quantity = 0; 
        }

        // Getters and Setters
        public int ProductId { get => productId; set => productId = value; }
        public string Name { get => name; set => name = value; }
        public decimal Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
        public int StockQuantity { get => stockQuantity; set => stockQuantity = value; }
        public int Quantity { get => quantity; set => quantity = value; } 
    }
}
