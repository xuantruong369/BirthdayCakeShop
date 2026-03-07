
USE BirthdayCakeShop;
GO

-- Users
INSERT INTO Users VALUES
('admin','123456','Admin'),
('staff01','123456','Staff'),
('truong123','123456','Customer'),
('lananh','123456','Customer'),
('minhpham','123456','Customer');

-- Customers
INSERT INTO Customers VALUES
('CUS001','truong123','Nguyen Van Truong','2003-05-12','0987654321','Ha Noi','VIP','truong.jpg','Frequent buyer'),
('CUS002','lananh','Tran Lan Anh','2002-09-21','0912345678','Hai Phong','Regular','lananh.jpg',''),
('CUS003','minhpham','Pham Minh','2001-01-10','0977777777','Da Nang','Regular','minh.jpg','');

-- Categories
INSERT INTO CakeCategories VALUES
('CAT01','Birthday Cake'),
('CAT02','Wedding Cake'),
('CAT03','Mini Cake'),
('CAT04','Kids Cake'),
('CAT05','Chocolate Cake');

-- Products
INSERT INTO Products VALUES
('P001','Chocolate Birthday Cake','CAT01','Two layer chocolate cake','cake1.jpg',200000,500000),
('P002','Strawberry Cream Cake','CAT01','Fresh strawberry cake','cake2.jpg',180000,450000),
('P003','Fruit Topping Cake','CAT01','Cake with fresh fruits','cake3.jpg',220000,480000),
('P004','Mini Chocolate Cake','CAT03','Small chocolate cake','cake4.jpg',90000,150000),
('P005','Kids Unicorn Cake','CAT04','Colorful unicorn cake for kids','cake5.jpg',300000,600000);

-- Product Details
INSERT INTO ProductDetails VALUES
('PD001','P001','16cm','Chocolate',250000,0,20),
('PD002','P001','20cm','Chocolate',350000,0,15),
('PD003','P002','16cm','Strawberry',230000,0,25),
('PD004','P002','20cm','Strawberry',340000,0,10),
('PD005','P003','20cm','Fruit',360000,0,10),
('PD006','P004','12cm','Chocolate',100000,0,30),
('PD007','P005','20cm','Vanilla',420000,0,8);

-- Product Images
INSERT INTO ProductImages (ProductId,ImageFile,DisplayOrder) VALUES
('P001','cake1_1.jpg',1),
('P001','cake1_2.jpg',2),
('P002','cake2_1.jpg',1),
('P003','cake3_1.jpg',1),
('P004','cake4_1.jpg',1),
('P005','cake5_1.jpg',1);

-- Orders
INSERT INTO Orders VALUES
('ORD001','2026-03-07','CUS001',500000,0,'COD','Deliver before 6PM'),
('ORD002','2026-03-07','CUS002',230000,0,'COD','Birthday party'),
('ORD003','2026-03-08','CUS003',360000,0,'Bank Transfer','Call before delivery');

-- Order Details
INSERT INTO OrderDetails (OrderId,ProductDetailId,Quantity,UnitPrice) VALUES
('ORD001','PD001',2,250000),
('ORD002','PD003',1,230000),
('ORD003','PD005',1,360000);
