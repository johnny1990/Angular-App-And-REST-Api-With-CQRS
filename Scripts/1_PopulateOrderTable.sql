SET NOCOUNT ON;
DECLARE @i INT = 1;

WHILE @i <= 100000
BEGIN
    INSERT INTO Orders (UserId, DateAdded)
    VALUES (
        FLOOR(RAND() * 10000) + 1,
        DATEADD(DAY, FLOOR(RAND() * 500), '2024-01-01')
    );
    SET @i += 1;
END
