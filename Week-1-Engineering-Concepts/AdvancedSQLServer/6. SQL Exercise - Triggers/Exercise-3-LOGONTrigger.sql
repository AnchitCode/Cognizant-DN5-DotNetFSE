

CREATE TRIGGER trg_RestrictLogon
ON ALL SERVER
FOR LOGON
AS
BEGIN
    IF DATEPART(HOUR, GETDATE()) >= 2
       AND DATEPART(HOUR, GETDATE()) < 3
    BEGIN
        ROLLBACK;

        RAISERROR
        (
            'Login is restricted during maintenance hours (2 AM - 3 AM).',
            16,
            1
        );
    END
END;
GO
