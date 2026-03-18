-- CATEGORY
INSERT INTO CakeCategories (CategoryName)
VALUES
(N'Bánh kem tươi'),
(N'Mousse'),
(N'Tiramisu');

DECLARE @kemtuoi INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Bánh kem tươi');
DECLARE @mousse INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Mousse');
DECLARE @tiramisu INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Tiramisu');

-- PRODUCTS
INSERT INTO Products (ProductName, CategoryId, Description, Thumbnail, IsActive, CreatedAt)
VALUES
-- KEM TUOI (1-15)
(N'Bánh kem tươi dâu',@kemtuoi,N'Dâu','kemtuoi1.jpg',1,GETDATE()),
(N'Bánh kem tươi socola',@kemtuoi,N'Socola','kemtuoi2.jpg',1,GETDATE()),
(N'Bánh kem tươi vani',@kemtuoi,N'Vani','kemtuoi3.jpg',1,GETDATE()),
(N'Bánh kem tươi matcha',@kemtuoi,N'Matcha','kemtuoi4.jpg',1,GETDATE()),
(N'Bánh kem tươi cam',@kemtuoi,N'Cam','kemtuoi5.jpg',1,GETDATE()),
(N'Bánh kem tươi xoài',@kemtuoi,N'Xoài','kemtuoi6.jpg',1,GETDATE()),
(N'Bánh kem tươi việt quất',@kemtuoi,N'Blueberry','kemtuoi7.jpg',1,GETDATE()),
(N'Bánh kem tươi dứa',@kemtuoi,N'Dứa','kemtuoi8.jpg',1,GETDATE()),
(N'Bánh kem tươi nho',@kemtuoi,N'Nho','kemtuoi9.jpg',1,GETDATE()),
(N'Bánh kem tươi oreo',@kemtuoi,N'Oreo','kemtuoi10.jpg',1,GETDATE()),
(N'Bánh kem tươi caramel',@kemtuoi,N'Caramel','kemtuoi11.jpg',1,GETDATE()),
(N'Bánh kem tươi phô mai',@kemtuoi,N'Cheese','kemtuoi12.jpg',1,GETDATE()),
(N'Bánh kem tươi trà xanh',@kemtuoi,N'Trà xanh','kemtuoi13.jpg',1,GETDATE()),
(N'Bánh kem tươi mini',@kemtuoi,N'Mini','kemtuoi14.jpg',1,GETDATE()),
(N'Bánh kem tươi đặc biệt',@kemtuoi,N'Special','kemtuoi15.jpg',1,GETDATE()),

-- MOUSSE (16-30)
(N'Mousse dâu',@mousse,N'Dâu','mousse1.jpg',1,GETDATE()),
(N'Mousse socola',@mousse,N'Socola','mousse2.jpg',1,GETDATE()),
(N'Mousse chanh dây',@mousse,N'Chanh dây','mousse3.jpg',1,GETDATE()),
(N'Mousse xoài',@mousse,N'Xoài','mousse4.jpg',1,GETDATE()),
(N'Mousse việt quất',@mousse,N'Blueberry','mousse5.jpg',1,GETDATE()),
(N'Mousse cam',@mousse,N'Cam','mousse6.jpg',1,GETDATE()),
(N'Mousse dừa',@mousse,N'Dừa','mousse7.jpg',1,GETDATE()),
(N'Mousse matcha',@mousse,N'Matcha','mousse8.jpg',1,GETDATE()),
(N'Mousse oreo',@mousse,N'Oreo','mousse9.jpg',1,GETDATE()),
(N'Mousse caramel',@mousse,N'Caramel','mousse10.jpg',1,GETDATE()),
(N'Mousse phô mai',@mousse,N'Cheese','mousse11.jpg',1,GETDATE()),
(N'Mousse trà xanh',@mousse,N'Trà xanh','mousse12.jpg',1,GETDATE()),
(N'Mousse mini',@mousse,N'Mini','mousse13.jpg',1,GETDATE()),
(N'Mousse cao cấp',@mousse,N'Luxury','mousse14.jpg',1,GETDATE()),
(N'Mousse đặc biệt',@mousse,N'Special','mousse15.jpg',1,GETDATE()),

-- TIRAMISU (31-45)
(N'Tiramisu truyền thống',@tiramisu,N'Classic','tiramisu1.jpg',1,GETDATE()),
(N'Tiramisu socola',@tiramisu,N'Socola','tiramisu2.jpg',1,GETDATE()),
(N'Tiramisu matcha',@tiramisu,N'Matcha','tiramisu3.jpg',1,GETDATE()),
(N'Tiramisu dâu',@tiramisu,N'Dâu','tiramisu4.jpg',1,GETDATE()),
(N'Tiramisu xoài',@tiramisu,N'Xoài','tiramisu5.jpg',1,GETDATE()),
(N'Tiramisu việt quất',@tiramisu,N'Blueberry','tiramisu6.jpg',1,GETDATE()),
(N'Tiramisu oreo',@tiramisu,N'Oreo','tiramisu7.jpg',1,GETDATE()),
(N'Tiramisu caramel',@tiramisu,N'Caramel','tiramisu8.jpg',1,GETDATE()),
(N'Tiramisu phô mai',@tiramisu,N'Cheese','tiramisu9.jpg',1,GETDATE()),
(N'Tiramisu trà xanh',@tiramisu,N'Trà xanh','tiramisu10.jpg',1,GETDATE()),
(N'Tiramisu mini',@tiramisu,N'Mini','tiramisu11.jpg',1,GETDATE()),
(N'Tiramisu cao cấp',@tiramisu,N'Luxury','tiramisu12.jpg',1,GETDATE()),
(N'Tiramisu trái cây',@tiramisu,N'Fruit','tiramisu13.jpg',1,GETDATE()),
(N'Tiramisu ít ngọt',@tiramisu,N'Less sugar','tiramisu14.jpg',1,GETDATE()),
(N'Tiramisu đặc biệt',@tiramisu,N'Special','tiramisu15.jpg',1,GETDATE());

--productdetail
DELETE FROM ProductDetails

INSERT INTO ProductDetails (ProductId, CakeSize, Flavor, Price, Discount, Stock)
SELECT ProductId, 'S', N'Vani', 150000, 0, 10 FROM Products
UNION ALL
SELECT ProductId, 'M', N'Socola', 250000, 5, 10 FROM Products
UNION ALL
SELECT ProductId, 'L', N'Dâu', 350000, 10, 10 FROM Products

--productimage
INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
SELECT 
    ProductId,
    CONCAT('Bánh kem tươi', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.jpg'),
    1
FROM Products
WHERE CategoryId = 1

UNION ALL

SELECT 
    ProductId,
    CONCAT('Bánh kem tươi', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.jpg'),
    2
FROM Products
WHERE CategoryId = 1
INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
SELECT 
    ProductId,
    CONCAT('Mousse', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.jpg'),
    1
FROM Products
WHERE CategoryId = 2

UNION ALL

SELECT 
    ProductId,
    CONCAT('Mousse', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.jpg'),
    2
FROM Products
WHERE CategoryId = 2
INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
SELECT 
    ProductId,
    CONCAT('Tiramisu', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.jpg'),
    1
FROM Products
WHERE CategoryId = 3

UNION ALL

SELECT 
    ProductId,
    CONCAT('Tiramisu', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.jpg'),
    2
FROM Products
WHERE CategoryId = 3