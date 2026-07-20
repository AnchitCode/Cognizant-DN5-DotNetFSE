
SELECT
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantity
FROM Orders o
JOIN OrderDetails od
    ON o.OrderID = od.OrderID
JOIN Customers c
    ON o.CustomerID = c.CustomerID
JOIN Products p
    ON od.ProductID = p.ProductID
GROUP BY GROUPING SETS
(
    (c.Region, p.Category),
    (c.Region),
    (p.Category)
);


-- ROLLUP

SELECT
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantity
FROM Orders o
JOIN OrderDetails od
    ON o.OrderID = od.OrderID
JOIN Customers c
    ON o.CustomerID = c.CustomerID
JOIN Products p
    ON od.ProductID = p.ProductID
GROUP BY ROLLUP
(
    c.Region,
    p.Category
);


-- CUBE

SELECT
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantity
FROM Orders o
JOIN OrderDetails od
    ON o.OrderID = od.OrderID
JOIN Customers c
    ON o.CustomerID = c.CustomerID
JOIN Products p
    ON od.ProductID = p.ProductID
GROUP BY CUBE
(
    c.Region,
    p.Category
);
