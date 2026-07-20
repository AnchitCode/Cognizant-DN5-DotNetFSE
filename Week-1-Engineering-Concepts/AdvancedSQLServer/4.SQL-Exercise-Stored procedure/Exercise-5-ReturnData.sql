

CREATE PROCEDURE sp_GetEmployeeCountByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT
        COUNT(*) AS TotalEmployees
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO


-- Execute Stored Procedure

EXEC sp_GetEmployeeCountByDepartment
    @DepartmentID = 1;
GO
