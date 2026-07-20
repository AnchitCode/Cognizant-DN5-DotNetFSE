-- =============================================
-- Exercise 4: Employee Report View
-- Objective:
-- Create a view that displays employee details
-- along with computed columns for AnnualSalary
-- and Bonus.
-- =============================================

CREATE VIEW vw_EmployeeReport
AS
SELECT
    e.EmployeeID,
    e.FirstName + ' ' + e.LastName AS FullName,
    d.DepartmentName,
    e.Salary * 12 AS AnnualSalary,
    (e.Salary * 12) * 0.10 AS Bonus
FROM Employees AS e
INNER JOIN Departments AS d
    ON e.DepartmentID = d.DepartmentID;


-- View Result

SELECT *
FROM vw_EmployeeReport;
