

CREATE TRIGGER trg_PreventEmployeeDeletion
ON Employees
INSTEAD OF DELETE
AS
BEGIN
    RAISERROR
    (
        'Deletion of employee records is not allowed.',
        16,
        1
    );
END;
GO


-- Test the Trigger

DELETE FROM Employees
WHERE EmployeeID = 1;
GO
