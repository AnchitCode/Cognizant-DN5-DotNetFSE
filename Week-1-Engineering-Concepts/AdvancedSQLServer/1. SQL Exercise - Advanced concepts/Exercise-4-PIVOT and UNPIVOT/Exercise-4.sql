-- =============================================
-- Exercise 4: PIVOT and UNPIVOT
-- =============================================

-- Aggregate Monthly Sales

WITH MonthlySales AS
(
    SELECT
        p.ProductName,
        MONTH(o.OrderDate) AS SalesMonth,
        SUM(od.Quantity) AS TotalQuantity
    FROM Orders o
    JOIN OrderDetails od
        ON o.OrderID = od.OrderID
    JOIN Products p
        ON od.ProductID = p.ProductID
    GROUP BY
        p.ProductName,
        MONTH(o.OrderDate)
)

-- PIVOT

SELECT *
FROM MonthlySales
PIVOT
(
    SUM(TotalQuantity)
    FOR SalesMonth IN
    (
        [1],[2],[3],[4],[5],[6],
        [7],[8],[9],[10],[11],[12]
    )
) AS PivotTable;


-- UNPIVOT

WITH MonthlySales AS
(
    SELECT
        p.ProductName,
        MONTH(o.OrderDate) AS SalesMonth,
        SUM(od.Quantity) AS TotalQuantity
    FROM Orders o
    JOIN OrderDetails od
        ON o.OrderID = od.OrderID
    JOIN Products p
        ON od.ProductID = p.ProductID
    GROUP BY
        p.ProductName,
        MONTH(o.OrderDate)
),
PivotResult AS
(
    SELECT *
    FROM MonthlySales
    PIVOT
    (
        SUM(TotalQuantity)
        FOR SalesMonth IN
        (
            [1],[2],[3],[4],[5],[6],
            [7],[8],[9],[10],[11],[12]
        )
    ) AS PivotTable
)

SELECT
    ProductName,
    SalesMonth,
    TotalQuantity
FROM PivotResult
UNPIVOT
(
    TotalQuantity FOR SalesMonth IN
    (
        [1],[2],[3],[4],[5],[6],
        [7],[8],[9],[10],[11],[12]
    )
) AS UnpivotTable;
