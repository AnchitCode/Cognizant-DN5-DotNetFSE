

CREATE OR ALTER PROCEDURE TransferEmployee
(
    @EmployeeID INT,
    @DepartmentID INT
)
AS
BEGIN
    BEGIN TRY

        BEGIN TRY

            IF NOT EXISTS
            (
                SELECT 1
                FROM Departments
                WHERE DepartmentID = @DepartmentID
            )
            BEGIN
                RAISERROR
                (
                    'Department does not exist.',
                    16,
                    1
                );
            END

            UPDATE Employees
            SET DepartmentID = @DepartmentID
            WHERE EmployeeID = @EmployeeID;

        END TRY

        BEGIN CATCH

            INSERT INTO AuditLog
            (
                Action,
                ErrorMessage
            )
            VALUES
            (
                'TransferEmployee',
                ERROR_MESSAGE()
            );

            THROW;

        END CATCH

    END TRY

    BEGIN CATCH

        PRINT 'Outer CATCH: ' + ERROR_MESSAGE();

    END CATCH
END;
GO


-- Test

EXEC TransferEmployee
    @EmployeeID = 1,
    @DepartmentID = 999;
GO


SELECT *
FROM AuditLog;
GO
