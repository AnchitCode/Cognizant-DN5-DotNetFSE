-- =============================================
-- Exercise 2: Computed Column - Full Name
-- Objective:
-- Create a view that displays employee details
-- with a computed FullName column.
-- =============================================

CREATE VIEW vw_EmployeeFullName
AS
SELECT
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    e.FirstName + ' ' + e.LastName AS FullName,
    d.DepartmentName
FROM Employees AS e
INNER JOIN Departments AS d
    ON e.DepartmentID = d.DepartmentID;


-- View Result

SELECT *
FROM vw_EmployeeFullName;
