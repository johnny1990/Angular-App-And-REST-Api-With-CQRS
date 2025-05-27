SELECT TOP 1000 u.Id, u.Name, SUM(op.Quantity * op.Price) AS TotalAmount
FROM Users u
JOIN Orders o ON u.Id = o.UserId
JOIN OrderProducts op ON o.Id = op.OrderId
WHERE o.DateAdded >= DATEADD(MONTH, -6, GETUTCDATE())
GROUP BY u.Id, u.Name
HAVING SUM(op.Quantity * op.Price) > 1000
ORDER BY TotalAmount DESC;
-----