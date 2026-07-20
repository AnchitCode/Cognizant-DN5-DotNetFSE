

CREATE PROCEDURE sp_GiveBonus
    @DepartmentID INT,
    @BonusAmount DECIMAL(10,2)
AS
BEGIN
    IF EXISTS
    (
        SELECT 1
        FROM Employees
        WHERE DepartmentID = @DepartmentID
    )
    BEGIN
        UPDATE Employees
        SET Salary = Salary + @BonusAmount
        WHERE DepartmentID = @DepartmentID;

        PRINT 'Bonus applied successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Department not found or has no employees.';
    END
END;
GO


-- Execute Stored Procedure

EXEC sp_GiveBonus
    @DepartmentID = 1,
    @BonusAmount = 500.00;
GO


-- Verify the Update

SELECT *
FROM Employees
WHERE DepartmentID = 1;
GO
