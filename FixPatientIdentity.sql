-- SQL Script to fix Patient ID identity seed
-- This ensures that when you delete patients and add new ones, the IDs continue from the highest existing ID

-- First, find the current maximum patient ID
DECLARE @MaxId INT;
SELECT @MaxId = ISNULL(MAX(patient_id), 0) FROM patients;

PRINT 'Current maximum patient_id: ' + CAST(@MaxId AS VARCHAR);

-- Reset the identity seed to the next available number
IF @MaxId >= 0
BEGIN
    DBCC CHECKIDENT ('patients', RESEED, @MaxId);
    PRINT 'Identity seed reset to: ' + CAST(@MaxId AS VARCHAR);
    PRINT 'Next patient will be assigned ID: ' + CAST((@MaxId + 1) AS VARCHAR);
END
ELSE
BEGIN
    DBCC CHECKIDENT ('patients', RESEED, 0);
    PRINT 'Identity seed reset to: 0';
    PRINT 'Next patient will be assigned ID: 1';
END

-- Verify the current identity value
DBCC CHECKIDENT ('patients', NORESEED);

PRINT 'Patient identity seed has been fixed successfully!';
PRINT 'New patients will now continue from the highest existing ID.';
