-- SQL Script to reset patient_id to start from 1
-- This will delete all existing patients and reset the identity to start from 1

-- WARNING: This will DELETE all existing patient records!
-- Make sure you have a backup before running this script

-- First, delete all existing patient records
DELETE FROM patients;

-- Reset the identity seed to 0 (next record will be ID 1)
DBCC CHECKIDENT ('patients', RESEED, 0);

-- Verify the reset
DBCC CHECKIDENT ('patients', NORESEED);

PRINT 'Patient table has been completely reset!';
PRINT 'Next patient added will have ID: 1';
PRINT 'All existing patient records have been deleted.';
