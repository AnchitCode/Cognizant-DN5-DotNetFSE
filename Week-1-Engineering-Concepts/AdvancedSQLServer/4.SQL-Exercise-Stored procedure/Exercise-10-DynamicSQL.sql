

CREATE PROCEDURE sp_GetEmployeesByFilter
    @FilterColumn NVARCHAR(50),
    @FilterValue NVARCHAR(100)
AS
BEGIN
    DECLARE @SqlQuery NVARCHAR(MAX);

    SET @SqlQuery =
        N'SELECT *
          FROM Employees
          WHERE ' + QUOTENAME(@FilterColumn) + N' = @Value';

    EXEC sp_executesql
        @SqlQuery,
        N'@Value NVARCHAR(100)',
        @Value = @FilterValue;
END;
GO


-- Execute Stored Procedure

EXEC sp_GetEmployeesByFilter
    @FilterColumn = 'FirstName',
    @FilterValue = 'John';
GO
