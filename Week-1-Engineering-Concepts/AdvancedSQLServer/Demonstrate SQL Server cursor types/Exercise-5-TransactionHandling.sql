

CREATE OR ALTER PROCEDURE BatchInsertEmployees
AS
BEGIN
    BEGIN TRY

        BEGIN TRANSACTION;

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
            20,
            'Alice',
            'Brown',
            'alice@example.com',
            5000,
            1
        );

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
            21,
            'Bob',
            'Wilson',
            'alice@example.com',   -- Duplicate Email
            5500,
            2
        );

        COMMIT TRANSACTION;

    END TRY

    BEGIN CATCH

        ROLLBACK TRANSACTION;

        INSERT INTO AuditLog
        (
            Action,
            ErrorMessage
        )
        VALUES
        (
            'BatchInsertEmployees',
            ERROR_MESSAGE()
        );

        THROW;

    END CATCH
END;
GO


-- Test

EXEC BatchInsertEmployees;
GO


SELECT *
FROM AuditLog;
GO
