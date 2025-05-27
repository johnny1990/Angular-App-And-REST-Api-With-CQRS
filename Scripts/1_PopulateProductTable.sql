SET NOCOUNT ON;
DECLARE @i INT = 1;

WHILE @i <= 10000
BEGIN
    INSERT INTO Products (Name, Price)
    VALUES (CONCAT('Product ', @i), ROUND(RAND() * (500 - 200) + 200, 2));
    SET @i += 1;
END
