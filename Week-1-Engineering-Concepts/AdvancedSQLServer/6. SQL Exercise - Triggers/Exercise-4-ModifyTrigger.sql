


ALTER TRIGGER trg_AfterSalaryUpdate
ON Employees
AFTER UPDATE
AS
BEGIN
    INSERT INTO EmployeeChanges
    (
        EmployeeID,
        OldSalary,
        NewSalary,
        ChangeDate
    )
    SELECT
        d.EmployeeID,
        d.Salary,
        i.Salary,
        GETDATE()
    FROM deleted AS d
    INNER JOIN inserted AS i
        ON d.EmployeeID = i.EmployeeID
    WHERE d.Salary <> i.Salary
       OR d.DepartmentID <> i.DepartmentID;
END;
GO


-- Test the Modified Trigger

UPDATE Employees
SET
    Salary = 8000.00,
    DepartmentID = 2
WHERE EmployeeID = 1;
GO


-- Verify Log

SELECT *
FROM EmployeeChanges;
GO
