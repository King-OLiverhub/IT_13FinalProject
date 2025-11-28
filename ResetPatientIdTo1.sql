-- SQL Script to reset patient_id to start from 1
-- Run this when you want to reset the counter back to 1

-- Reset the identity seed to 0 (next patient will get ID 1)
DBCC CHECKIDENT ('patients', RESEED, 0);

-- Verify the current identity value
DBCC CHECKIDENT ('patients', NORESEED);

PRINT 'Patient ID has been reset!';
PRINT 'Next patient added will have ID: 1';
PRINT 'Current identity value is: 0 (next will be 1)';
