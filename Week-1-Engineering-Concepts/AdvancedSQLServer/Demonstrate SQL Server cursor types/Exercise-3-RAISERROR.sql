

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

        IF @Salary <= 0
        BEGIN
            RAISERROR
            (
                'Salary must be greater than zero.',
                16,
                1
            );
            RETURN;
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


-- Test

EXEC AddEmployee
    @EmployeeID = 12,
    @FirstName = 'Mike',
    @LastName = 'Ross',
    @Email = 'mike@example.com',
    @Salary = -500,
    @DepartmentID = 1;
GO
