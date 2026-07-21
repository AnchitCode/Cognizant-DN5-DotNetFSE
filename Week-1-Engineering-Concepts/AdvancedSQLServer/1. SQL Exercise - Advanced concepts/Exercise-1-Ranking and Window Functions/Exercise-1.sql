-- Exercise 1: Ranking and Window Functions

SELECT
    ProductID,
    ProductName,
    Category,
    Price,
    ROW_NUMBER() OVER (
        PARTITION BY Category
        ORDER BY Price DESC
    ) AS RowNum,
    RANK() OVER (
        PARTITION BY Category
        ORDER BY Price DESC
    ) AS ProductRank,
    DENSE_RANK() OVER (
        PARTITION BY Category
        ORDER BY Price DESC
    ) AS DenseProductRank
FROM Products;


-- Top 3 most expensive products in each category

WITH RankedProducts AS
(
    SELECT
        ProductID,
        ProductName,
        Category,
        Price,
        ROW_NUMBER() OVER
        (
            PARTITION BY Category
            ORDER BY Price DESC
        ) AS RowNum
    FROM Products
)

SELECT *
FROM RankedProducts
WHERE RowNum <= 3;
