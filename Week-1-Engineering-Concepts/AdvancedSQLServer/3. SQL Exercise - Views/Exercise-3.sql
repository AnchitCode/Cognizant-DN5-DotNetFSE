-- =============================================
-- Exercise 3: Computed Column - Annual Salary
-- Objective:
-- Create a view that displays employee details
-- along with the computed AnnualSalary column.
-- =============================================

CREATE VIEW vw_EmployeeAnnualSalary
AS
SELECT
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    d.DepartmentName,
    e.Salary,
    e.Salary * 12 AS AnnualSalary
FROM Employees AS e
INNER JOIN Departments AS d
    ON e.DepartmentID = d.DepartmentID;


-- View Result

SELECT *
FROM vw_EmployeeAnnualSalary;
