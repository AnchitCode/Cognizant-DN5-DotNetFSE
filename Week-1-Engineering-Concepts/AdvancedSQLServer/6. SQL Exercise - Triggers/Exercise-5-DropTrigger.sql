

DROP TRIGGER IF EXISTS trg_AfterSalaryUpdate;
GO


-- Verify the Trigger Has Been Deleted

SELECT
    name AS TriggerName
FROM sys.triggers
WHERE name = 'trg_AfterSalaryUpdate';
GO
