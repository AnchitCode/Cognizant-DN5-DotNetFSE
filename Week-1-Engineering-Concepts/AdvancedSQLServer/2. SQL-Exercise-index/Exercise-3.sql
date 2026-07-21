-- =============================================
-- Exercise 3: Creating a Composite Index
-- Objective:
-- Create a composite index on CustomerID and
-- OrderDate to optimize queries filtering on
-- both columns.
-- =============================================

-- Before Creating Index

SELECT *
FROM Orders
WHERE CustomerID = 1
  AND OrderDate = '2023-01-15';


-- Create Composite Index

CREATE NONCLUSTERED INDEX IX_Orders_CustomerID_OrderDate
ON Orders(CustomerID, OrderDate);


-- After Creating Index

SELECT *
FROM Orders
WHERE CustomerID = 1
  AND OrderDate = '2023-01-15';
