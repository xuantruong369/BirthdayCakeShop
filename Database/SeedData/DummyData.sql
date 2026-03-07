USE BirthdayCakeShop;
GO

-- 1. Chèn dữ liệu Tài khoản (Mật khẩu giả định đã hash)
INSERT INTO Users (Username, PasswordHash, Role) VALUES 
('admin', 'hashed_password_123', 'Admin'),
('staff_lan', 'hashed_password_456', 'Staff'),
('customer_minh', 'hashed_password_789', 'Customer'),
('customer_hoa', 'hashed_password_000', 'Customer');

-- 2. Chèn dữ liệu Khách hàng
INSERT INTO Customers (UserId, CustomerName, Phone, BirthDate, Address, CustomerType) VALUES 
(3, N'Nguyễn Văn Minh', '0901234567', '1995-05-15', N'123 Lê Lợi, Quận 1, TP.HCM', N'Vàng'),
(4, N'Lê Thị Hoa', '0988777666', '1998-10-20', N'456 Nguyễn Huệ, Quận 1, TP.HCM', N'Bạc');

-- 3. Chèn Danh mục bánh
INSERT INTO CakeCategories (CategoryName, Description) VALUES 
(N'Bánh Kem Truyền Thống', N'Các dòng bánh gato kem tươi'),
(N'Bánh Tiramisu', N'Dòng bánh Ý hương vị cafe'),
(N'Bánh Mousse', N'Bánh ít ngọt, cốt bánh mịn màng');

-- 4. Chèn Sản phẩm
INSERT INTO Products (ProductName, CategoryId, Description, Thumbnail) VALUES 
(N'Bánh Kem Dâu Tây', 1, N'Bánh kem tươi trang trí dâu tây Đà Lạt', 'strawberry_cake.jpg'),
(N'Tiramisu Cổ Điển', 2, N'Hương vị cà phê nồng nàn từ Ý', 'tiramisu.jpg'),
(N'Mousse Socola', 3, N'Socola đen nguyên chất 70%', 'chocolat_mousse.jpg');

-- 5. Chèn Chi tiết Sản phẩm (Kích thước & Hương vị)
INSERT INTO ProductDetails (ProductId, CakeSize, Flavor, Price, Stock) VALUES 
(1, '16cm', N'Vani', 250000, 10),
(1, '20cm', N'Vani', 350000, 5),
(2, 'Standard', N'Coffee', 300000, 8),
(3, '18cm', N'Chocolate', 400000, 12);

-- 6. Chèn Mã giảm giá (Voucher)
INSERT INTO Vouchers (VoucherCode, DiscountValue, IsPercentage, MinOrderAmount, StartDate, EndDate) VALUES 
('CHUC_MUNG', 50000, 0, 200000, '2024-01-01', '2025-12-31'),
('GIAM_10', 10, 1, 100000, '2024-01-01', '2025-12-31');

-- 7. Chèn Giỏ hàng mẫu
INSERT INTO Cart (CustomerId) VALUES (1);
INSERT INTO CartItems (CartId, ProductDetailId, Quantity, CakeText) VALUES 
(1, 1, 1, N'Happy Birthday My Love');

-- 8. Chèn Đơn hàng mẫu
INSERT INTO Orders (CustomerId, TotalAmount, VoucherId, ActualAmount, PaymentMethod, Status, ShippingAddress, DeliveryDate, DeliveryTimeSlot) VALUES 
(1, 250000, 1, 200000, 'COD', 'Processing', N'123 Lê Lợi, Quận 1, TP.HCM', '2024-03-20', '14:00 - 15:00');

-- 9. Chèn Chi tiết đơn hàng
INSERT INTO OrderItems (OrderId, ProductDetailId, Quantity, UnitPrice, CakeText, TotalPrice) VALUES 
(1, 1, 1, 250000, N'Happy Birthday My Love', 250000);

-- 10. Chèn Lịch sử đơn hàng
INSERT INTO OrderStatusHistory (OrderId, Status, Note) VALUES 
(1, 'Pending', N'Khách vừa đặt hàng'),
(1, 'Processing', N'Đang trong lò nướng');

-- 11. Chèn Đánh giá
INSERT INTO Reviews (ProductId, CustomerId, Rating, Comment) VALUES 
(1, 1, 5, N'Bánh rất ngon, dâu tươi!');
GO
