
DROP FUNCTION IF EXISTS fn_CalculateBonus;
GO


-- Verify the Function Has Been Deleted

SELECT OBJECT_ID('fn_CalculateBonus') AS FunctionID;
GO
