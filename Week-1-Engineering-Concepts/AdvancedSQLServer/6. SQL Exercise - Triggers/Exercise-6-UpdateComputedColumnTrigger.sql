

-- Add AnnualSalary Column

ALTER TABLE Employees
ADD AnnualSalary DECIMAL(10,2);
GO


-- Initialize Existing Records

UPDATE Employees
SET AnnualSalary = Salary * 12;
GO


-- Create Trigger

CREATE TRIGGER trg_UpdateAnnualSalary
ON Employees
AFTER UPDATE
AS
BEGIN
    UPDATE e
    SET e.AnnualSalary = i.Salary * 12
    FROM Employees AS e
    INNER JOIN inserted AS i
        ON e.EmployeeID = i.EmployeeID
    WHERE i.Salary <> e.AnnualSalary / 12;
END;
GO


-- Test the Trigger

UPDATE Employees
SET Salary = 8000.00
WHERE EmployeeID = 1;
GO


-- Verify the Result

SELECT
    EmployeeID,
    FirstName,
    LastName,
    Salary,
    AnnualSalary
FROM Employees;
GO
