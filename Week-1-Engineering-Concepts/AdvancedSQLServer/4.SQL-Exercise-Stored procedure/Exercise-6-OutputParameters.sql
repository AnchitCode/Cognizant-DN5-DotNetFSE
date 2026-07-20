
CREATE PROCEDURE sp_GetTotalSalaryByDepartment
    @DepartmentID INT,
    @TotalSalary DECIMAL(10,2) OUTPUT
AS
BEGIN
    SELECT
        @TotalSalary = SUM(Salary)
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO


-- Execute Stored Procedure

DECLARE @Salary DECIMAL(10,2);

EXEC sp_GetTotalSalaryByDepartment
    @DepartmentID = 1,
    @TotalSalary = @Salary OUTPUT;

SELECT @Salary AS TotalSalary;
GO
