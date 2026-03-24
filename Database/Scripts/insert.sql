USE BirthdayCakeShop;
GO 

INSERT INTO Users (Username, PasswordHash, Role) 
VALUES 
('admin', 'admin', 'Admin'),
('truong', 'truong', 'Customer');
GO  

-- 2. Chèn dữ liệu Khách hàng
INSERT INTO Customers (UserId, CustomerName, Phone, BirthDate, Address, CustomerType) VALUES 
(1, N'Nguyễn Văn Minh', '0901234567', '1995-05-15', N'123 Lê Lợi, Quận 1, TP.HCM', N'Vàng'),
(2, N'Lê Thị Hoa', '0988777666', '1998-10-20', N'456 Nguyễn Huệ, Quận 1, TP.HCM', N'Bạc');
GO 
-- CATEGORY
INSERT INTO CakeCategories (CategoryName)
VALUES
(N'Bánh kem tươi'),
(N'Mousse'),
(N'Tiramisu'),
(N'Bánh trái cây'),
(N'Bánh fondant'),
(N'Cupcake sinh nhật');
GO  

DECLARE @kemtuoi INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Bánh kem tươi');
DECLARE @mousse INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Mousse');
DECLARE @tiramisu INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Tiramisu');
DECLARE @fruit INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Bánh trái cây');
DECLARE @fon INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Bánh fondant');
DECLARE @cup INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Cupcake sinh nhật');

-- PRODUCTS
INSERT INTO Products (ProductName, CategoryId, Description, Thumbnail, IsActive, CreatedAt)
VALUES
-- KEM TUOI (1-15)
(N'Bánh kem tươi dâu',@kemtuoi,N'Dâu','kemtuoi1.png',1,GETDATE()),
(N'Bánh kem tươi socola',@kemtuoi,N'Socola','kemtuoi2.png',1,GETDATE()),
(N'Bánh kem tươi vani',@kemtuoi,N'Vani','kemtuoi3.png',1,GETDATE()),
(N'Bánh kem tươi matcha',@kemtuoi,N'Matcha','kemtuoi4.png',1,GETDATE()),
(N'Bánh kem tươi cam',@kemtuoi,N'Cam','kemtuoi5.png',1,GETDATE()),
(N'Bánh kem tươi xoài',@kemtuoi,N'Xoài','kemtuoi6.png',1,GETDATE()),
(N'Bánh kem tươi việt quất',@kemtuoi,N'Blueberry','kemtuoi7.png',1,GETDATE()),
(N'Bánh kem tươi dứa',@kemtuoi,N'Dứa','kemtuoi8.png',1,GETDATE()),
(N'Bánh kem tươi nho',@kemtuoi,N'Nho','kemtuoi9.png',1,GETDATE()),
(N'Bánh kem tươi oreo',@kemtuoi,N'Oreo','kemtuoi10.png',1,GETDATE()),
(N'Bánh kem tươi caramel',@kemtuoi,N'Caramel','kemtuoi11.png',1,GETDATE()),
(N'Bánh kem tươi phô mai',@kemtuoi,N'Cheese','kemtuoi12.png',1,GETDATE()),
(N'Bánh kem tươi trà xanh',@kemtuoi,N'Trà xanh','kemtuoi13.png',1,GETDATE()),
(N'Bánh kem tươi mini',@kemtuoi,N'Mini','kemtuoi14.png',1,GETDATE()),
(N'Bánh kem tươi đặc biệt',@kemtuoi,N'Special','kemtuoi15.png',1,GETDATE()),

-- MOUSSE (16-30)
(N'Mousse dâu',@mousse,N'Dâu','mousse1.png',1,GETDATE()),
(N'Mousse socola',@mousse,N'Socola','mousse2.png',1,GETDATE()),
(N'Mousse chanh dây',@mousse,N'Chanh dây','mousse3.png',1,GETDATE()),
(N'Mousse xoài',@mousse,N'Xoài','mousse4.png',1,GETDATE()),
(N'Mousse việt quất',@mousse,N'Blueberry','mousse5.png',1,GETDATE()),
(N'Mousse cam',@mousse,N'Cam','mousse6.png',1,GETDATE()),
(N'Mousse dừa',@mousse,N'Dừa','mousse7.png',1,GETDATE()),
(N'Mousse matcha',@mousse,N'Matcha','mousse8.png',1,GETDATE()),
(N'Mousse oreo',@mousse,N'Oreo','mousse9.png',1,GETDATE()),
(N'Mousse caramel',@mousse,N'Caramel','mousse10.png',1,GETDATE()),
(N'Mousse phô mai',@mousse,N'Cheese','mousse11.png',1,GETDATE()),
(N'Mousse trà xanh',@mousse,N'Trà xanh','mousse12.png',1,GETDATE()),
(N'Mousse mini',@mousse,N'Mini','mousse13.png',1,GETDATE()),
(N'Mousse cao cấp',@mousse,N'Luxury','mousse14.png',1,GETDATE()),
(N'Mousse đặc biệt',@mousse,N'Special','mousse15.png',1,GETDATE()),

-- TIRAMISU (31-45)
(N'Tiramisu truyền thống',@tiramisu,N'Classic','tiramisu1.png',1,GETDATE()),
(N'Tiramisu socola',@tiramisu,N'Socola','tiramisu2.png',1,GETDATE()),
(N'Tiramisu matcha',@tiramisu,N'Matcha','tiramisu3.png',1,GETDATE()),
(N'Tiramisu dâu',@tiramisu,N'Dâu','tiramisu4.png',1,GETDATE()),
(N'Tiramisu xoài',@tiramisu,N'Xoài','tiramisu5.png',1,GETDATE()),
(N'Tiramisu việt quất',@tiramisu,N'Blueberry','tiramisu6.jpg',1,GETDATE()),
(N'Tiramisu oreo',@tiramisu,N'Oreo','tiramisu7.png',1,GETDATE()),
(N'Tiramisu caramel',@tiramisu,N'Caramel','tiramisu8.png',1,GETDATE()),
(N'Tiramisu phô mai',@tiramisu,N'Cheese','tiramisu9.png',1,GETDATE()),
(N'Tiramisu trà xanh',@tiramisu,N'Trà xanh','tiramisu10.png',1,GETDATE()),
(N'Tiramisu mini',@tiramisu,N'Mini','tiramisu11.png',1,GETDATE()),
(N'Tiramisu cao cấp',@tiramisu,N'Luxury','tiramisu12.png',1,GETDATE()),
(N'Tiramisu trái cây',@tiramisu,N'Fruit','tiramisu13.png',1,GETDATE()),
(N'Tiramisu ít ngọt',@tiramisu,N'Less sugar','tiramisu14.jpg',1,GETDATE()),
(N'Tiramisu đặc biệt',@tiramisu,N'Special','tiramisu15.png',1,GETDATE()),

-- FRUIT (1-15)
(N'Bánh trái cây dâu',@fruit,N'Dâu','fruit1.jpg',1,GETDATE()),
(N'Bánh trái cây kiwi',@fruit,N'Kiwi','fruit2.jpg',1,GETDATE()),
(N'Bánh trái cây xoài',@fruit,N'Xoài','fruit3.jpg',1,GETDATE()),
(N'Bánh trái cây mix',@fruit,N'Mix','fruit4.jpg',1,GETDATE()),
(N'Bánh trái cây việt quất',@fruit,N'Blueberry','fruit5.jpg',1,GETDATE()),
(N'Bánh trái cây cam',@fruit,N'Cam','fruit6.jpg',1,GETDATE()),
(N'Bánh trái cây đào',@fruit,N'Đào','fruit7.jpg',1,GETDATE()),
(N'Bánh trái cây dứa',@fruit,N'Dứa','fruit8.jpg',1,GETDATE()),
(N'Bánh trái cây nho',@fruit,N'Nho','fruit9.jpg',1,GETDATE()),
(N'Bánh trái cây kem',@fruit,N'Kem','fruit10.jpg',1,GETDATE()),
(N'Bánh trái cây socola',@fruit,N'Socola','fruit11.jpg',1,GETDATE()),
(N'Bánh trái cây matcha',@fruit,N'Matcha','fruit12.jpg',1,GETDATE()),
(N'Bánh trái cây 2 tầng',@fruit,N'2 tầng','fruit13.jpg',1,GETDATE()),
(N'Bánh trái cây mini',@fruit,N'Mini','fruit14.jpg',1,GETDATE()),
(N'Bánh trái cây đặc biệt',@fruit,N'Special','fruit15.jpg',1,GETDATE()),

-- FONDANT (16-30)
(N'Fondant công chúa',@fon,N'Princess','fondant1.jpg',1,GETDATE()),
(N'Fondant siêu nhân',@fon,N'Superhero','fondant2.jpg',1,GETDATE()),
(N'Fondant hoạt hình',@fon,N'Cartoon','fondant3.jpg',1,GETDATE()),
(N'Fondant cưới',@fon,N'Wedding','fondant4.jpg',1,GETDATE()),
(N'Fondant 2 tầng',@fon,N'2 tầng','fondant5.jpg',1,GETDATE()),
(N'Fondant 3 tầng',@fon,N'3 tầng','fondant6.jpg',1,GETDATE()),
(N'Fondant hoa',@fon,N'Hoa','fondant7.jpg',1,GETDATE()),
(N'Fondant xe hơi',@fon,N'Xe','fondant8.jpg',1,GETDATE()),
(N'Fondant động vật',@fon,N'Thú','fondant9.jpg',1,GETDATE()),
(N'Fondant cao cấp',@fon,N'Luxury','fondant10.jpg',1,GETDATE()),
(N'Fondant trẻ em',@fon,N'Trẻ em','fondant11.jpg',1,GETDATE()),
(N'Fondant sinh nhật',@fon,N'SN','fondant12.jpg',1,GETDATE()),
(N'Fondant lễ hội',@fon,N'Lễ','fondant13.jpg',1,GETDATE()),
(N'Fondant đơn giản',@fon,N'Đơn giản','fondant14.jpg',1,GETDATE()),
(N'Fondant theo yêu cầu',@fon,N'Request','fondant15.jpg',1,GETDATE()),

-- CUPCAKE (31-45)
(N'Cupcake dâu',@cup,N'Dâu','cupcake1.jpg',1,GETDATE()),
(N'Cupcake socola',@cup,N'Socola','cupcake2.jpg',1,GETDATE()),
(N'Cupcake vani',@cup,N'Vani','cupcake3.jpg',1,GETDATE()),
(N'Cupcake matcha',@cup,N'Matcha','cupcake4.jpg',1,GETDATE()),
(N'Cupcake cam',@cup,N'Cam','cupcake5.jpg',1,GETDATE()),
(N'Cupcake oreo',@cup,N'Oreo','cupcake6.jpg',1,GETDATE()),
(N'Cupcake caramel',@cup,N'Caramel','cupcake7.jpg',1,GETDATE()),
(N'Cupcake phô mai',@cup,N'Cheese','cupcake8.jpg',1,GETDATE()),
(N'Cupcake dứa',@cup,N'Dứa','cupcake9.jpg',1,GETDATE()),
(N'Cupcake mix vị',@cup,N'Mix','cupcake10.jpg',1,GETDATE()),
(N'Cupcake mini',@cup,N'Mini','cupcake11.jpg',1,GETDATE()),
(N'Cupcake cao cấp',@cup,N'Luxury','cupcake12.jpg',1,GETDATE()),
(N'Cupcake cho bé',@cup,N'Kid','cupcake13.jpg',1,GETDATE()),
(N'Cupcake trang trí',@cup,N'Decor','cupcake14.jpg',1,GETDATE()),
(N'Cupcake đặc biệt',@cup,N'Special','cupcake15.jpg',1,GETDATE());
GO  

INSERT INTO ProductDetails (ProductId, CakeSize, Flavor, Price, Discount, Stock)
SELECT ProductId, 'S', N'Vani', 100000 + ABS(CHECKSUM(NEWID())) % 100000, 0, 10 FROM Products
UNION ALL
SELECT ProductId, 'M', N'Socola', 200000 + ABS(CHECKSUM(NEWID())) % 100000, 5, 10 FROM Products
UNION ALL
SELECT ProductId, 'L', N'Dâu', 300000 + ABS(CHECKSUM(NEWID())) % 100000, 10, 10 FROM Products
GO 

--productimage
INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
SELECT 
    ProductId,
    CONCAT('kemtuoi', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.png'),
    1
FROM Products
WHERE CategoryId = 1

UNION ALL

SELECT 
    ProductId,
    CONCAT('kemtuoi', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.png'),
    2
FROM Products
WHERE CategoryId = 1
GO  

INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
SELECT 
    ProductId,
    CONCAT('mousse', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.png'),
    1
FROM Products
WHERE CategoryId = 2

UNION ALL

SELECT 
    ProductId,
    CONCAT('mousse', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.png'),
    2
FROM Products
WHERE CategoryId = 2
GO  

INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
SELECT 
    ProductId,
    CONCAT('tiramisu', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.png'),
    1
FROM Products
WHERE CategoryId = 3

UNION ALL

SELECT 
    ProductId,
    CONCAT('tiramisu', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.png'),
    2
FROM Products
WHERE CategoryId = 3
GO 

INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
SELECT 
    ProductId,
    CONCAT('fruit', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.jpg'),
    1
FROM Products
WHERE CategoryId = 4

UNION ALL

SELECT 
    ProductId,
    CONCAT('fruit', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.jpg'),
    2
FROM Products
WHERE CategoryId = 4
GO  

INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
SELECT 
    ProductId,
    CONCAT('fondant', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.jpg'),
    1
FROM Products
WHERE CategoryId = 5

UNION ALL

SELECT 
    ProductId,
    CONCAT('fondant', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.jpg'),
    2
FROM Products
WHERE CategoryId = 5
GO  

INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
SELECT 
    ProductId,
    CONCAT('cupcake', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.jpg'),
    1
FROM Products
WHERE CategoryId = 6

UNION ALL

SELECT 
    ProductId,
    CONCAT('cupcake', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.jpg'),
    2
FROM Products
WHERE CategoryId = 6
