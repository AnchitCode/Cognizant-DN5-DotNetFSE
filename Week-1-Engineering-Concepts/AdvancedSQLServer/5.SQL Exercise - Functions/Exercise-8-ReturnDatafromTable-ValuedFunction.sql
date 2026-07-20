

SELECT
    EmployeeID,
    FirstName,
    LastName,
    DepartmentID,
    Salary,
    JoinDate
FROM dbo.fn_GetEmployeesByDepartment(3);
GO
