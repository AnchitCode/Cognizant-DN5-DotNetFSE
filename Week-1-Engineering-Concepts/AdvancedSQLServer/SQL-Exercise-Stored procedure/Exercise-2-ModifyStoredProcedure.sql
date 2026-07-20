

ALTER PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT
        EmployeeID,
        FirstName,
        LastName,
        DepartmentID,
        Salary,
        JoinDate
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO


-- Execute Stored Procedure

EXEC sp_GetEmployeesByDepartment @DepartmentID = 1;
