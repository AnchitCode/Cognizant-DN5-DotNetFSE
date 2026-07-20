

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

        IF @Salary < 0
        BEGIN
            RAISERROR
            (
                'Salary cannot be negative.',
                16,
                1
            );
            RETURN;
        END

        IF @Salary < 1000
        BEGIN
            RAISERROR
            (
                'Warning: Salary is below the recommended minimum.',
                10,
                1
            );
        END

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


-- Test 1 (Warning)

EXEC AddEmployee
    @EmployeeID = 30,
    @FirstName = 'Tom',
    @LastName = 'Hardy',
    @Email = 'tom@example.com',
    @Salary = 800,
    @DepartmentID = 1;
GO


-- Test 2 (Error)

EXEC AddEmployee
    @EmployeeID = 31,
    @FirstName = 'Chris',
    @LastName = 'Evans',
    @Email = 'chris@example.com',
    @Salary = -500,
    @DepartmentID = 1;
GO
