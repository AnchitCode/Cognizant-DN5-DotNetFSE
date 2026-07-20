

DROP PROCEDURE IF EXISTS sp_GetEmployeesByDepartment;
GO


-- Verify the Stored Procedure Has Been Dropped

EXEC sp_GetEmployeesByDepartment @DepartmentID = 1;
