-- =============================================
-- Exercise 1: Creating a Non-Clustered Index
-- Objective:
-- Create a non-clustered index on ProductName
-- and compare query execution before and after.
-- =============================================

-- Before Creating Index

SELECT *
FROM Products
WHERE ProductName = 'Laptop';


-- Create Non-Clustered Index

CREATE NONCLUSTERED INDEX IX_Products_ProductName
ON Products(ProductName);


-- After Creating Index

SELECT *
FROM Products
WHERE ProductName = 'Laptop';
