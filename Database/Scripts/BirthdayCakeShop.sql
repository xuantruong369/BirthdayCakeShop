CREATE DATABASE BirthdayCakeShop;
GO

USE BirthdayCakeShop;
GO

-- 1. Quản lý Tài khoản
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL, -- Admin, Customer
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 2. Thông tin khách hàng
CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT UNIQUE,
    CustomerName NVARCHAR(100),
    Phone NVARCHAR(20),
    BirthDate DATE,
    Address NVARCHAR(255),
    Avatar NVARCHAR(255),
    CustomerType NVARCHAR(50), -- Đồng, Bạc, Vàng...
    Note NVARCHAR(255),
    FOREIGN KEY (UserId) REFERENCES Users(UserId) -- 1-1
);

-- 3. Danh mục sản phẩm
CREATE TABLE CakeCategories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255)
);

-- 4. Thông tin Sản phẩm (Chung)
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(150) NOT NULL,
    CategoryId INT,
    Description NVARCHAR(MAX),
    Thumbnail NVARCHAR(255),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CategoryId) REFERENCES CakeCategories(CategoryId) -- N-1
);

-- 5. Chi tiết biến thể Sản phẩm (Kích thước, Hương vị, Giá)
CREATE TABLE ProductDetails (
    ProductDetailId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    CakeSize NVARCHAR(50), -- Ví dụ: 16cm, 20cm
    Flavor NVARCHAR(50),   -- Ví dụ: Tiramisu, Chocolate
    Price DECIMAL(10,2) NOT NULL,
    Discount DECIMAL(10,2) DEFAULT 0,
    Stock INT DEFAULT 0,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId) -- N-1
);

-- 6. Thư viện hình ảnh sản phẩm
CREATE TABLE ProductImages (
    ImageId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    ImageUrl NVARCHAR(255),
    DisplayOrder INT DEFAULT 1,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId) -- N-1
);

-- 7. Mã giảm giá (Voucher)
CREATE TABLE Vouchers (
    VoucherId INT IDENTITY(1,1) PRIMARY KEY,
    VoucherCode NVARCHAR(50) UNIQUE NOT NULL,
    DiscountValue DECIMAL(10,2) NOT NULL,
    IsPercentage BIT DEFAULT 0, -- 1: Giảm theo %, 0: Giảm tiền mặt
    MinOrderAmount DECIMAL(12,2) DEFAULT 0,
    StartDate DATETIME,
    EndDate DATETIME,
    UsageLimit INT DEFAULT 1,
    IsActive BIT DEFAULT 1
);

-- 8. Giỏ hàng
CREATE TABLE Cart (
    CartId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT UNIQUE,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId) -- 1-1
);

CREATE TABLE CartItems (
    CartItemId INT IDENTITY(1,1) PRIMARY KEY,
    CartId INT,
    ProductDetailId INT,
    Quantity INT,
    CakeText NVARCHAR(255), -- Nội dung ghi lên bánh khi đặt
    FOREIGN KEY (CartId) REFERENCES Cart(CartId), -- N-1
    FOREIGN KEY (ProductDetailId) REFERENCES ProductDetails(ProductDetailId) -- N-1
);

-- 9. Đơn hàng
CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(12,2),
    VoucherId INT,
    ActualAmount DECIMAL(12,2), -- Số tiền thực tế sau khi trừ voucher
    PaymentMethod NVARCHAR(50), -- COD, Bank Transfer, MoMo...
    Status NVARCHAR(50),        -- Pending, Processing, Delivering, Completed, Cancelled
    ShippingAddress NVARCHAR(255),
    DeliveryDate DATE,          -- Ngày nhận bánh khách chọn
    DeliveryTimeSlot NVARCHAR(50), -- Giờ nhận bánh khách chọn
    Note NVARCHAR(255),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),-- N-1
    FOREIGN KEY (VoucherId) REFERENCES Vouchers(VoucherId) -- N-1
);

-- 10. Chi tiết đơn hàng
CREATE TABLE OrderItems (
    OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT,
    ProductDetailId INT,
    Quantity INT,
    UnitPrice DECIMAL(10,2),
    CakeText NVARCHAR(255), -- Lưu lại nội dung ghi lên bánh lúc đặt
    TotalPrice DECIMAL(12,2),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),-- N-1
    FOREIGN KEY (ProductDetailId) REFERENCES ProductDetails(ProductDetailId) -- N-1
);

-- 11. Thanh toán
CREATE TABLE Payments (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT,
    Amount DECIMAL(12,2),
    PaymentMethod NVARCHAR(50),
    PaymentStatus NVARCHAR(50), -- Unpaid, Paid, Refunded
    PaymentDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) -- N-1
);

-- 12. Theo dõi lịch sử đơn hàng
CREATE TABLE OrderStatusHistory (
    HistoryId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    UpdateDate DATETIME DEFAULT GETDATE(),
    Note NVARCHAR(255),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) -- N-1
);

-- 13. Đánh giá sản phẩm
CREATE TABLE Reviews (
    ReviewId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    CustomerId INT NOT NULL,
    Rating INT CHECK (Rating >= 1 AND Rating <= 5),
    Comment NVARCHAR(MAX),
    ImageUrl NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId), -- N-1
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId) -- N-1
);
GO
