

CREATE PROCEDURE sp_UpdateSalaryWithErrorHandling
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    BEGIN TRY

        UPDATE Employees
        SET Salary = @NewSalary
        WHERE EmployeeID = @EmployeeID;

        PRINT 'Salary updated successfully.';

    END TRY

    BEGIN CATCH

        PRINT 'An error occurred while updating salary.';
        PRINT ERROR_MESSAGE();

    END CATCH
END;
GO


-- Execute Stored Procedure

EXEC sp_UpdateSalaryWithErrorHandling
    @EmployeeID = 1,
    @NewSalary = 7000.00;
GO


-- Verify the Update

SELECT *
FROM Employees
WHERE EmployeeID = 1;
GO
