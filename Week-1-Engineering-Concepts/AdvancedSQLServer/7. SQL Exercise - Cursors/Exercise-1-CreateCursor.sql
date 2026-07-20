

DECLARE
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @DepartmentID INT,
    @Salary DECIMAL(10,2),
    @JoinDate DATE;

DECLARE EmployeeCursor CURSOR
FOR
SELECT
    EmployeeID,
    FirstName,
    LastName,
    DepartmentID,
    Salary,
    JoinDate
FROM Employees;

OPEN EmployeeCursor;

FETCH NEXT FROM EmployeeCursor
INTO
    @EmployeeID,
    @FirstName,
    @LastName,
    @DepartmentID,
    @Salary,
    @JoinDate;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT
        'EmployeeID: ' + CAST(@EmployeeID AS VARCHAR(10))
        + ', Name: ' + @FirstName + ' ' + @LastName
        + ', DepartmentID: ' + CAST(@DepartmentID AS VARCHAR(10))
        + ', Salary: ' + CAST(@Salary AS VARCHAR(20));

    FETCH NEXT FROM EmployeeCursor
    INTO
        @EmployeeID,
        @FirstName,
        @LastName,
        @DepartmentID,
        @Salary,
        @JoinDate;
END;

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;
GO
