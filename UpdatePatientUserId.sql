-- SQL Script to allow NULL values in user_id column of patients table
-- This allows patients to be created without user accounts

-- First, update existing patient records that might have invalid user_id values
UPDATE patients 
SET user_id = NULL 
WHERE user_id NOT IN (SELECT user_id FROM users);

-- Alter the column to allow NULL values
ALTER TABLE patients 
ALTER COLUMN user_id INT NULL;

-- Optional: Add a comment to document the change
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Nullable user_id - patients can exist without user accounts', 
    @level0type = N'Schema', @level0name = N'dbo', 
    @level1type = N'Table', @level1name = N'patients', 
    @level2type = N'Column', @level2name = N'user_id';

PRINT 'Successfully updated patients table to allow NULL user_id values';
