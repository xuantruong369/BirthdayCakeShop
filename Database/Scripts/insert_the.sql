-- INSERT INTO CakeCategories (CategoryName)
-- VALUES
-- (N'Bánh trái cây'),
-- (N'Bánh fondant'),
-- (N'Cupcake sinh nh?t');

-- DECLARE @fruit INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Bánh trái cây')
-- DECLARE @fon INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Bánh fondant')
-- DECLARE @cup INT = (SELECT CategoryId FROM CakeCategories WHERE CategoryName = N'Cupcake sinh nh?t')
-- INSERT INTO Products (ProductName, CategoryId, Description, Thumbnail, IsActive, CreatedAt)
-- VALUES
-- -- FRUIT (1-15)
-- (N'Bánh trái cây dâu',@fruit,N'Dâu','fruit1.jpg',1,GETDATE()),
-- (N'Bánh trái cây kiwi',@fruit,N'Kiwi','fruit2.jpg',1,GETDATE()),
-- (N'Bánh trái cây xoài',@fruit,N'Xoài','fruit3.jpg',1,GETDATE()),
-- (N'Bánh trái cây mix',@fruit,N'Mix','fruit4.jpg',1,GETDATE()),
-- (N'Bánh trái cây vi?t qu?t',@fruit,N'Blueberry','fruit5.jpg',1,GETDATE()),
-- (N'Bánh trái cây cam',@fruit,N'Cam','fruit6.jpg',1,GETDATE()),
-- (N'Bánh trái cây đào',@fruit,N'Đào','fruit7.jpg',1,GETDATE()),
-- (N'Bánh trái cây d?a',@fruit,N'D?a','fruit8.jpg',1,GETDATE()),
-- (N'Bánh trái cây nho',@fruit,N'Nho','fruit9.jpg',1,GETDATE()),
-- (N'Bánh trái cây kem',@fruit,N'Kem','fruit10.jpg',1,GETDATE()),
-- (N'Bánh trái cây socola',@fruit,N'Socola','fruit11.jpg',1,GETDATE()),
-- (N'Bánh trái cây matcha',@fruit,N'Matcha','fruit12.jpg',1,GETDATE()),
-- (N'Bánh trái cây 2 t?ng',@fruit,N'2 t?ng','fruit13.jpg',1,GETDATE()),
-- (N'Bánh trái cây mini',@fruit,N'Mini','fruit14.jpg',1,GETDATE()),
-- (N'Bánh trái cây đ?c bi?t',@fruit,N'Special','fruit15.jpg',1,GETDATE()),

-- -- FONDANT (16-30)
-- (N'Fondant công chúa',@fon,N'Princess','fon1.jpg',1,GETDATE()),
-- (N'Fondant siêu nhân',@fon,N'Superhero','fon2.jpg',1,GETDATE()),
-- (N'Fondant ho?t h?nh',@fon,N'Cartoon','fon3.jpg',1,GETDATE()),
-- (N'Fondant cư?i',@fon,N'Wedding','fon4.jpg',1,GETDATE()),
-- (N'Fondant 2 t?ng',@fon,N'2 t?ng','fon5.jpg',1,GETDATE()),
-- (N'Fondant 3 t?ng',@fon,N'3 t?ng','fon6.jpg',1,GETDATE()),
-- (N'Fondant hoa',@fon,N'Hoa','fon7.jpg',1,GETDATE()),
-- (N'Fondant xe hơi',@fon,N'Xe','fon8.jpg',1,GETDATE()),
-- (N'Fondant đ?ng v?t',@fon,N'Thú','fon9.jpg',1,GETDATE()),
-- (N'Fondant cao c?p',@fon,N'Luxury','fon10.jpg',1,GETDATE()),
-- (N'Fondant tr? em',@fon,N'Tr? em','fon11.jpg',1,GETDATE()),
-- (N'Fondant sinh nh?t',@fon,N'SN','fon12.jpg',1,GETDATE()),
-- (N'Fondant l? h?i',@fon,N'L?','fon13.jpg',1,GETDATE()),
-- (N'Fondant đơn gi?n',@fon,N'Đơn gi?n','fon14.jpg',1,GETDATE()),
-- (N'Fondant theo yêu c?u',@fon,N'Request','fon15.jpg',1,GETDATE()),

-- -- CUPCAKE (31-45)
-- (N'Cupcake dâu',@cup,N'Dâu','cup1.jpg',1,GETDATE()),
-- (N'Cupcake socola',@cup,N'Socola','cup2.jpg',1,GETDATE()),
-- (N'Cupcake vani',@cup,N'Vani','cup3.jpg',1,GETDATE()),
-- (N'Cupcake matcha',@cup,N'Matcha','cup4.jpg',1,GETDATE()),
-- (N'Cupcake cam',@cup,N'Cam','cup5.jpg',1,GETDATE()),
-- (N'Cupcake oreo',@cup,N'Oreo','cup6.jpg',1,GETDATE()),
-- (N'Cupcake caramel',@cup,N'Caramel','cup7.jpg',1,GETDATE()),
-- (N'Cupcake phô mai',@cup,N'Cheese','cup8.jpg',1,GETDATE()),
-- (N'Cupcake d?a',@cup,N'D?a','cup9.jpg',1,GETDATE()),
-- (N'Cupcake mix v?',@cup,N'Mix','cup10.jpg',1,GETDATE()),
-- (N'Cupcake mini',@cup,N'Mini','cup11.jpg',1,GETDATE()),
-- (N'Cupcake cao c?p',@cup,N'Luxury','cup12.jpg',1,GETDATE()),
-- (N'Cupcake cho bé',@cup,N'Kid','cup13.jpg',1,GETDATE()),
-- (N'Cupcake trang trí',@cup,N'Decor','cup14.jpg',1,GETDATE()),
-- (N'Cupcake đ?c bi?t',@cup,N'Special','cup15.jpg',1,GETDATE());

-- --productdetail
-- DELETE FROM ProductDetails

-- INSERT INTO ProductDetails (ProductId, CakeSize, Flavor, Price, Discount, Stock)
-- SELECT ProductId, 'S', N'Vani', 150000, 0, 10 FROM Products
-- UNION ALL
-- SELECT ProductId, 'M', N'Socola', 250000, 5, 10 FROM Products
-- UNION ALL
-- SELECT ProductId, 'L', N'Dâu', 350000, 10, 10 FROM Products

-- --productimage
-- INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
-- SELECT 
--     ProductId,
--     CONCAT('fruit', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.jpg'),
--     1
-- FROM Products
-- WHERE CategoryId = 1

-- UNION ALL

-- SELECT 
--     ProductId,
--     CONCAT('fruit', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.jpg'),
--     2
-- FROM Products
-- WHERE CategoryId = 1
-- INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
-- SELECT 
--     ProductId,
--     CONCAT('fon', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.jpg'),
--     1
-- FROM Products
-- WHERE CategoryId = 2

-- UNION ALL

-- SELECT 
--     ProductId,
--     CONCAT('fon', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.jpg'),
--     2
-- FROM Products
-- WHERE CategoryId = 2
-- INSERT INTO ProductImages (ProductId, ImageUrl, DisplayOrder)
-- SELECT 
--     ProductId,
--     CONCAT('cup', ROW_NUMBER() OVER (ORDER BY ProductId), '_1.jpg'),
-- FROM Products
-- WHERE CategoryId = 3

-- UNION ALL

-- SELECT 
--     ProductId,
--     CONCAT('cup', ROW_NUMBER() OVER (ORDER BY ProductId), '_2.jpg'),
--     2
-- FROM Products
-- WHERE CategoryId = 3
