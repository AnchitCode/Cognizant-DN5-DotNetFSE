-- =============================================
-- Exercise 3: CTEs and MERGE
-- =============================================

-- Part A: Recursive CTE to Generate Calendar Dates

WITH CalendarCTE AS
(
    SELECT CAST('2025-01-01' AS DATE) AS CalendarDate

    UNION ALL

    SELECT DATEADD(DAY, 1, CalendarDate)
    FROM CalendarCTE
    WHERE CalendarDate < '2025-01-31'
)

SELECT *
FROM CalendarCTE
OPTION (MAXRECURSION 0);


-- Part B: Create Staging Table

CREATE TABLE StagingProducts
(
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10,2)
);


-- Sample Data

INSERT INTO StagingProducts
VALUES
(1, 'Laptop Pro', 'Laptop', 95000),
(2, 'Gaming Mouse', 'Accessories', 2500),
(10, 'Mechanical Keyboard', 'Accessories', 5500);


-- MERGE Statement

MERGE Products AS Target
USING StagingProducts AS Source
ON Target.ProductID = Source.ProductID

WHEN MATCHED THEN
    UPDATE SET
        Target.ProductName = Source.ProductName,
        Target.Category = Source.Category,
        Target.Price = Source.Price

WHEN NOT MATCHED THEN
    INSERT
    (
        ProductID,
        ProductName,
        Category,
        Price
    )
    VALUES
    (
        Source.ProductID,
        Source.ProductName,
        Source.Category,
        Source.Price
    );
