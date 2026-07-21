

-- =============================================
-- STATIC CURSOR
-- Snapshot of data. Does not reflect changes.
-- =============================================

DECLARE StaticCursor CURSOR STATIC
FOR
SELECT *
FROM Employees;

OPEN StaticCursor;
CLOSE StaticCursor;
DEALLOCATE StaticCursor;
GO


-- =============================================
-- DYNAMIC CURSOR
-- Reflects all INSERT, UPDATE and DELETE changes.
-- =============================================

DECLARE DynamicCursor CURSOR DYNAMIC
FOR
SELECT *
FROM Employees;

OPEN DynamicCursor;
CLOSE DynamicCursor;
DEALLOCATE DynamicCursor;
GO


-- =============================================
-- FORWARD_ONLY CURSOR
-- Can move only in the forward direction.
-- Fastest and least memory usage.
-- =============================================

DECLARE ForwardCursor CURSOR FORWARD_ONLY
FOR
SELECT *
FROM Employees;

OPEN ForwardCursor;
CLOSE ForwardCursor;
DEALLOCATE ForwardCursor;
GO


-- =============================================
-- KEYSET CURSOR
-- Keys are fixed.
-- Updates are visible.
-- New rows are not visible.
-- =============================================

DECLARE KeysetCursor CURSOR KEYSET
FOR
SELECT *
FROM Employees;

OPEN KeysetCursor;
CLOSE KeysetCursor;
DEALLOCATE KeysetCursor;
GO
