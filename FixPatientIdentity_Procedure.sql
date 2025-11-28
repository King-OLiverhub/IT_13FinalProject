-- Create a stored procedure to automatically fix patient identity seed
-- You can run this whenever you notice ID issues

CREATE OR ALTER PROCEDURE sp_FixPatientIdentity
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @MaxId INT;
    DECLARE @CurrentIdentity INT;
    
    -- Get the maximum patient ID
    SELECT @MaxId = ISNULL(MAX(patient_id), 0) FROM patients;
    
    -- Get the current identity value
    DBCC CHECKIDENT ('patients', NORESEED) WITH NO_INFOMSGS;
    
    -- Set identity to the max value (it will increment to max+1 on next insert)
    DBCC CHECKIDENT ('patients', RESEED, @MaxId) WITH NO_INFOMSGS;
    
    -- Get the new identity value
    DBCC CHECKIDENT ('patients', NORESEED) WITH NO_INFOMSGS;
    
    SELECT 
        @MaxId AS MaxPatientId,
        (@MaxId + 1) AS NextPatientId,
        'Patient identity seed has been fixed. Next patient will get ID: ' + CAST((@MaxId + 1) AS VARCHAR) AS Message;
END

-- How to use:
-- EXEC sp_FixPatientIdentity

-- Test the procedure
EXEC sp_FixPatientIdentity;
