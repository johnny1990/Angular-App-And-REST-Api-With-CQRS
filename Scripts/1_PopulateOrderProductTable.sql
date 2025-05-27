SET NOCOUNT ON;
DECLARE @i INT = 1;

WHILE @i <= 300000
BEGIN
    INSERT INTO OrderProducts (OrderId, ProductId, Quantity, Price)
    VALUES (
        FLOOR(RAND() * 100000) + 1,
        FLOOR(RAND() * 10000) + 1,
        FLOOR(RAND() * 5) + 1,
        ROUND(RAND() * (500 - 200) + 200, 2)
    );
    SET @i += 1;
END
