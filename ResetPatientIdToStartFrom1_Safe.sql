-- SQL Script to reset patient_id to start from 1 while preserving existing data
-- This approach creates a temporary table, preserves data, resets identity, then restores data

-- Step 1: Create a temporary table to store existing patients
CREATE TABLE #temp_patients (
    patient_id INT,
    user_id INT NULL,
    first_name NVARCHAR(100),
    last_name NVARCHAR(100),
    middle_name NVARCHAR(100) NULL,
    gender NVARCHAR(10) NULL,
    date_of_birth DATETIME NULL,
    phone NVARCHAR(20) NULL,
    email NVARCHAR(100) NULL,
    address NVARCHAR(MAX) NULL,
    blood_type NVARCHAR(5) NULL,
    emergency_contact_name NVARCHAR(100) NULL,
    emergency_contact_relationship NVARCHAR(50) NULL,
    emergency_contact_number NVARCHAR(20) NULL,
    civil_status NVARCHAR(20) NULL,
    occupation NVARCHAR(100) NULL,
    nationality NVARCHAR(50) NULL,
    religion NVARCHAR(50) NULL,
    created_at DATETIME NULL,
    updated_at DATETIME NULL
);

-- Step 2: Copy existing data to temporary table (excluding the identity column)
INSERT INTO #temp_patients (
    user_id, first_name, last_name, middle_name, gender, date_of_birth,
    phone, email, address, blood_type, emergency_contact_name,
    emergency_contact_relationship, emergency_contact_number, civil_status,
    occupation, nationality, religion, created_at, updated_at
)
SELECT 
    user_id, first_name, last_name, middle_name, gender, date_of_birth,
    phone, email, address, blood_type, emergency_contact_name,
    emergency_contact_relationship, emergency_contact_number, civil_status,
    occupation, nationality, religion, created_at, updated_at
FROM patients;

-- Step 3: Delete all records from original table
DELETE FROM patients;

-- Step 4: Reset identity to start from 0 (next record will be ID 1)
DBCC CHECKIDENT ('patients', RESEED, 0);

-- Step 5: Insert data back with new sequential IDs starting from 1
INSERT INTO patients (
    user_id, first_name, last_name, middle_name, gender, date_of_birth,
    phone, email, address, blood_type, emergency_contact_name,
    emergency_contact_relationship, emergency_contact_number, civil_status,
    occupation, nationality, religion, created_at, updated_at
)
SELECT 
    user_id, first_name, last_name, middle_name, gender, date_of_birth,
    phone, email, address, blood_type, emergency_contact_name,
    emergency_contact_relationship, emergency_contact_number, civil_status,
    occupation, nationality, religion, created_at, updated_at
FROM #temp_patients
ORDER BY created_at; -- Preserve original order based on creation date

-- Step 6: Drop temporary table
DROP TABLE #temp_patients;

-- Step 7: Verify the result
SELECT 
    patient_id, 
    first_name, 
    last_name, 
    created_at
FROM patients 
ORDER BY patient_id;

PRINT 'Patient IDs have been reset to start from 1!';
PRINT 'Existing patient data has been preserved.';
PRINT 'All patients now have sequential IDs starting from 1.';
