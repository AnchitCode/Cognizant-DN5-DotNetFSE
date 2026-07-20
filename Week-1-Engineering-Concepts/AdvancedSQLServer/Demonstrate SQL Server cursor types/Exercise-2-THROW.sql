

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

        THROW;
    END CATCH
END;
GO


-- Test (Duplicate Email)

EXEC AddEmployee
    @EmployeeID = 11,
    @FirstName = 'Jane',
    @LastName = 'Doe',
    @Email = 'john@example.com', -- Duplicate Email
    @Salary = 6000,
    @DepartmentID = 1;
GO
