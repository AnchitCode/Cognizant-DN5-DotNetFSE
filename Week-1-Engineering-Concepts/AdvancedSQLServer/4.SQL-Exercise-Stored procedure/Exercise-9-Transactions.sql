

CREATE PROCEDURE sp_UpdateSalaryWithTransaction
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Employees
        SET Salary = @NewSalary
        WHERE EmployeeID = @EmployeeID;

        COMMIT TRANSACTION;

        PRINT 'Salary updated successfully.';
    END TRY

    BEGIN CATCH
        ROLLBACK TRANSACTION;

        PRINT 'Transaction failed.';
        PRINT ERROR_MESSAGE();
    END CATCH
END;
GO


-- Execute Stored Procedure

EXEC sp_UpdateSalaryWithTransaction
    @EmployeeID = 1,
    @NewSalary = 6500.00;
GO


-- Verify the Update

SELECT *
FROM Employees
WHERE EmployeeID = 1;
GO
