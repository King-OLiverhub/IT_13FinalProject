-- Create a stored procedure to reset patient ID to start from 1
-- You can run this anytime you want to reset the counter

CREATE OR ALTER PROCEDURE sp_ResetPatientIdTo1
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Reset the identity seed to 0 (next patient will get ID 1)
    DBCC CHECKIDENT ('patients', RESEED, 0) WITH NO_INFOMSGS;
    
    -- Get the current identity value to confirm
    DECLARE @CurrentIdentity INT;
    
    SELECT 
        'Patient ID has been reset successfully!' AS Message,
        'Next patient added will have ID: 1' AS NextId,
        'Current identity value is: 0 (next will be 1)' AS CurrentValue;
END

-- How to use:
-- EXEC sp_ResetPatientIdTo1

-- Test the procedure
EXEC sp_ResetPatientIdTo1;
