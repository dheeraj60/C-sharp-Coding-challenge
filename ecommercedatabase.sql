-- Create the database
CREATE DATABASE ecommercedatabase;
GO

-- Use the created database
USE ecommercedatabase;
GO

-- Create customers table
CREATE TABLE customers (
    customer_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) UNIQUE NOT NULL,
    password NVARCHAR(100) NOT NULL
);
GO

-- Create products table
CREATE TABLE products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    price DECIMAL(18, 2) NOT NULL,
    description NVARCHAR(500),
    stockQuantity INT NOT NULL
);
GO

-- Create cart table
CREATE TABLE cart (
    cart_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT FOREIGN KEY REFERENCES customers(customer_id),
    product_id INT FOREIGN KEY REFERENCES products(product_id),
    quantity INT NOT NULL
);
GO

-- Create orders table
CREATE TABLE orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT FOREIGN KEY REFERENCES customers(customer_id),
    order_date DATETIME NOT NULL DEFAULT GETDATE(),
    total_price DECIMAL(18, 2) NOT NULL,
    shipping_address NVARCHAR(255) NOT NULL
);
GO

-- Create order_items table
CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT FOREIGN KEY REFERENCES orders(order_id),
    product_id INT FOREIGN KEY REFERENCES products(product_id),
    quantity INT NOT NULL
);
GO

select * from customers;
select * from cart;

INSERT INTO customers (name, email, password) VALUES
('John Doe', 'john.doe@example.com', 'password123'),
('Jane Smith', 'jane.smith@example.com', 'password456'),
('Alice Johnson', 'alice.johnson@example.com', 'password789'),
('Bob Brown', 'bob.brown@example.com', 'password321'),
('Charlie Green', 'charlie.green@example.com', 'password654'),
('Daisy White', 'daisy.white@example.com', 'password987'),
('Ethan Black', 'ethan.black@example.com', 'password159');

INSERT INTO products (name, price, description, stockQuantity) VALUES
('Laptop', 799.99, 'High performance laptop', 50),
('Smartphone', 499.99, 'Latest smartphone with excellent features', 100),
('Headphones', 89.99, 'Noise-cancelling over-ear headphones', 200),
('Smartwatch', 199.99, 'Stylish smartwatch with fitness tracking', 75),
('Camera', 599.99, 'DSLR camera with high resolution', 30),
('Tablet', 299.99, 'Portable tablet with 10-inch display', 60),
('Charger', 29.99, 'Fast charging USB wall charger', 150);

INSERT INTO cart (customer_id, product_id, quantity) VALUES
(1, 1, 1),  -- John Doe adds 1 Laptop
(1, 2, 2),  -- John Doe adds 2 Smartphones
(2, 3, 1),  -- Jane Smith adds 1 Headphone
(3, 4, 1),  -- Alice Johnson adds 1 Smartwatch
(3, 5, 1),  -- Alice Johnson adds 1 Camera
(4, 6, 1),  -- Bob Brown adds 1 Tablet
(5, 7, 2);  -- Charlie Green adds 2 Charge

INSERT INTO orders (customer_id, total_price, shipping_address) VALUES
(1, 1299.97, '123 Main St, Springfield, IL'),
(2, 89.99, '456 Elm St, Springfield, IL'),
(3, 799.99, '789 Oak St, Springfield, IL'),
(4, 299.99, '321 Pine St, Springfield, IL'),
(5, 199.99, '654 Maple St, Springfield, IL'),
(6, 499.99, '987 Cedar St, Springfield, IL'),
(7, 29.99, '159 Birch St, Springfield, IL');

INSERT INTO order_items (order_id, product_id, quantity) VALUES
(1, 1, 1),  -- Order 1: 1 Laptop
(1, 2, 2),  -- Order 1: 2 Smartphones
(2, 3, 1),  -- Order 2: 1 Headphone
(3, 4, 1),  -- Order 3: 1 Smartwatch
(4, 5, 1),  -- Order 4: 1 Camera
(5, 6, 1),  -- Order 5: 1 Tablet
(6, 7, 2);  -- Order 6: 2 Chargers

select * from customers;
select * from cart;
select * from order_items;
select * from orders;
select * from products;









