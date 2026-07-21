-- =============================================
-- Exercise 2: Creating a Clustered Index
-- Objective:
-- Create a clustered index on the OrderDate
-- column and compare query execution before
-- and after index creation.
-- =============================================

-- Before Creating Index

SELECT *
FROM Orders
WHERE OrderDate = '2023-01-15';


-- Create Clustered Index

CREATE CLUSTERED INDEX IX_Orders_OrderDate
ON Orders(OrderDate);


-- After Creating Index

SELECT *
FROM Orders
WHERE OrderDate = '2023-01-15';
