CREATE DATABASE BirthdayCakeShop;
GO

USE BirthdayCakeShop;
GO

CREATE TABLE Users (
    Username NVARCHAR(50) PRIMARY KEY,
    Password NVARCHAR(100) NOT NULL,
    UserRole NVARCHAR(20) NOT NULL
);

CREATE TABLE Customers (
    CustomerId NVARCHAR(10) PRIMARY KEY,
    Username NVARCHAR(50),
    CustomerName NVARCHAR(100),
    BirthDate DATE,
    Phone NVARCHAR(15),
    Address NVARCHAR(255),
    CustomerType NVARCHAR(20),
    Avatar NVARCHAR(255),
    Note NVARCHAR(255),
    FOREIGN KEY (Username) REFERENCES Users(Username)
);

CREATE TABLE CakeCategories (
    CategoryId NVARCHAR(10) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);

CREATE TABLE Products (
    ProductId NVARCHAR(10) PRIMARY KEY,
    ProductName NVARCHAR(150) NOT NULL,
    CategoryId NVARCHAR(10),
    Description NVARCHAR(500),
    Thumbnail NVARCHAR(255),
    MinPrice DECIMAL(10,2),
    MaxPrice DECIMAL(10,2),
    FOREIGN KEY (CategoryId) REFERENCES CakeCategories(CategoryId)
);

CREATE TABLE ProductDetails (
    ProductDetailId NVARCHAR(10) PRIMARY KEY,
    ProductId NVARCHAR(10),
    CakeSize NVARCHAR(20),
    Flavor NVARCHAR(50),
    Price DECIMAL(10,2),
    Discount DECIMAL(5,2),
    Stock INT,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE ProductImages (
    ImageId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId NVARCHAR(10),
    ImageFile NVARCHAR(255),
    DisplayOrder INT,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE Orders (
    OrderId NVARCHAR(10) PRIMARY KEY,
    OrderDate DATE,
    CustomerId NVARCHAR(10),
    TotalAmount DECIMAL(10,2),
    Discount DECIMAL(5,2),
    PaymentMethod NVARCHAR(50),
    Note NVARCHAR(255),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

CREATE TABLE OrderDetails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId NVARCHAR(10),
    ProductDetailId NVARCHAR(10),
    Quantity INT,
    UnitPrice DECIMAL(10,2),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductDetailId) REFERENCES ProductDetails(ProductDetailId)
);