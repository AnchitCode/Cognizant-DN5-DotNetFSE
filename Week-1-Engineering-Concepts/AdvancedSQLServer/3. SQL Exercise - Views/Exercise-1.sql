-- =============================================
-- Exercise 1: Create a Simple View
-- Objective:
-- Create a view to display basic employee
-- information along with department name.
-- =============================================

CREATE VIEW vw_EmployeeBasicInfo
AS
SELECT
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    d.DepartmentName
FROM Employees AS e
INNER JOIN Departments AS d
    ON e.DepartmentID = d.DepartmentID;


-- View Result

SELECT *
FROM vw_EmployeeBasicInfo;
