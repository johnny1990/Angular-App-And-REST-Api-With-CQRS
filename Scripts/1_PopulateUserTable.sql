SET NOCOUNT ON;
DECLARE @i INT = 1;

WHILE @i <= 10000
BEGIN
    INSERT INTO Users (Name)
    VALUES (CONCAT('User ', @i));
    SET @i += 1;
END
