-- =============================================

CREATE OR ALTER PROCEDURE AddEmployee
(
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
)
AS
BEGIN
    BEGIN TRY
        INSERT INTO Employees
        (
            EmployeeID,
            FirstName,
            LastName,
            Email,
            Salary,
            DepartmentID
        )
        VALUES
        (
            @EmployeeID,
            @FirstName,
            @LastName,
            @Email,
            @Salary,
            @DepartmentID
        );
    END TRY

    BEGIN CATCH
        INSERT INTO AuditLog
        (
            Action,
            ErrorMessage
        )
        VALUES
        (
            'AddEmployee',
            ERROR_MESSAGE()
        );
    END CATCH
END;
GO


-- Test

EXEC AddEmployee
    @EmployeeID = 10,
    @FirstName = 'John',
    @LastName = 'Doe',
    @Email = 'john@example.com',
    @Salary = 5000,
    @DepartmentID = 1;
GO


SELECT *
FROM AuditLog;
GO
