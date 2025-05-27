CREATE INDEX IDX_Orders_UserId_DateAdded
ON Orders (UserId, DateAdded);

CREATE INDEX IDX_OrderProducts_OrderId_ProductId
ON OrderProducts (OrderId, ProductId);

CREATE UNIQUE INDEX IDX_Users_Id
ON Users (Id);



CREATE INDEX IDX_Orders_DateAdded 
ON Orders(DateAdded);

CREATE INDEX IDX_Orders_DateAdded_UserId
ON Orders (DateAdded, UserId);
