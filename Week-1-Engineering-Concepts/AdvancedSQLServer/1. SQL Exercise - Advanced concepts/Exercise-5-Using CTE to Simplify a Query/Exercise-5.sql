-- =============================================
-- Exercise 5: Using CTE to Simplify a Query
-- =============================================

WITH CustomerOrderCounts AS
(
    SELECT
        o.CustomerID,
        COUNT(o.OrderID) AS OrderCount
    FROM Orders o
    GROUP BY o.CustomerID
)

SELECT
    c.CustomerID,
    c.Name,
    coc.OrderCount
FROM CustomerOrderCounts AS coc
JOIN Customers AS c
    ON c.CustomerID = coc.CustomerID
WHERE coc.OrderCount > 3;
