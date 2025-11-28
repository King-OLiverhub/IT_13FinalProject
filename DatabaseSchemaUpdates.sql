-- Comprehensive Database Schema Updates
-- This script adds all missing columns to existing tables based on the enhanced models

-- ============================================
-- DOCTORS TABLE UPDATES
-- ============================================

-- Add missing columns to doctors table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'doctors' AND COLUMN_NAME = 'middle_name')
BEGIN
    ALTER TABLE doctors ADD middle_name NVARCHAR(100) NULL;
    PRINT 'Added middle_name to doctors table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'doctors' AND COLUMN_NAME = 'department')
BEGIN
    ALTER TABLE doctors ADD department NVARCHAR(100) NULL;
    PRINT 'Added department to doctors table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'doctors' AND COLUMN_NAME = 'years_of_experience')
BEGIN
    ALTER TABLE doctors ADD years_of_experience INT NULL;
    PRINT 'Added years_of_experience to doctors table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'doctors' AND COLUMN_NAME = 'education')
BEGIN
    ALTER TABLE doctors ADD education NVARCHAR(500) NULL;
    PRINT 'Added education to doctors table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'doctors' AND COLUMN_NAME = 'consultation_fee')
BEGIN
    ALTER TABLE doctors ADD consultation_fee DECIMAL(10, 2) NULL;
    PRINT 'Added consultation_fee to doctors table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'doctors' AND COLUMN_NAME = 'is_available')
BEGIN
    ALTER TABLE doctors ADD is_available BIT DEFAULT 1;
    PRINT 'Added is_available to doctors table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'doctors' AND COLUMN_NAME = 'created_at')
BEGIN
    ALTER TABLE doctors ADD created_at DATETIME NULL;
    PRINT 'Added created_at to doctors table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'doctors' AND COLUMN_NAME = 'updated_at')
BEGIN
    ALTER TABLE doctors ADD updated_at DATETIME NULL;
    PRINT 'Added updated_at to doctors table';
END

-- ============================================
-- APPOINTMENTS TABLE UPDATES
-- ============================================

-- Add missing columns to appointments table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'appointments' AND COLUMN_NAME = 'reason_for_visit')
BEGIN
    ALTER TABLE appointments ADD reason_for_visit NVARCHAR(500) NULL;
    PRINT 'Added reason_for_visit to appointments table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'appointments' AND COLUMN_NAME = 'symptoms')
BEGIN
    ALTER TABLE appointments ADD symptoms NVARCHAR(1000) NULL;
    PRINT 'Added symptoms to appointments table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'appointments' AND COLUMN_NAME = 'is_urgent')
BEGIN
    ALTER TABLE appointments ADD is_urgent BIT DEFAULT 0;
    PRINT 'Added is_urgent to appointments table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'appointments' AND COLUMN_NAME = 'payment_status')
BEGIN
    ALTER TABLE appointments ADD payment_status NVARCHAR(20) NULL;
    PRINT 'Added payment_status to appointments table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'appointments' AND COLUMN_NAME = 'created_at')
BEGIN
    ALTER TABLE appointments ADD created_at DATETIME NULL;
    PRINT 'Added created_at to appointments table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'appointments' AND COLUMN_NAME = 'updated_at')
BEGIN
    ALTER TABLE appointments ADD updated_at DATETIME NULL;
    PRINT 'Added updated_at to appointments table';
END

-- ============================================
-- NURSES TABLE UPDATES
-- ============================================

-- Add missing columns to nurses table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'nurses' AND COLUMN_NAME = 'middle_name')
BEGIN
    ALTER TABLE nurses ADD middle_name NVARCHAR(100) NULL;
    PRINT 'Added middle_name to nurses table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'nurses' AND COLUMN_NAME = 'department')
BEGIN
    ALTER TABLE nurses ADD department NVARCHAR(100) NULL;
    PRINT 'Added department to nurses table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'nurses' AND COLUMN_NAME = 'years_of_experience')
BEGIN
    ALTER TABLE nurses ADD years_of_experience INT NULL;
    PRINT 'Added years_of_experience to nurses table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'nurses' AND COLUMN_NAME = 'shift')
BEGIN
    ALTER TABLE nurses ADD shift NVARCHAR(20) NULL;
    PRINT 'Added shift to nurses table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'nurses' AND COLUMN_NAME = 'is_on_duty')
BEGIN
    ALTER TABLE nurses ADD is_on_duty BIT DEFAULT 1;
    PRINT 'Added is_on_duty to nurses table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'nurses' AND COLUMN_NAME = 'created_at')
BEGIN
    ALTER TABLE nurses ADD created_at DATETIME NULL;
    PRINT 'Added created_at to nurses table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'nurses' AND COLUMN_NAME = 'updated_at')
BEGIN
    ALTER TABLE nurses ADD updated_at DATETIME NULL;
    PRINT 'Added updated_at to nurses table';
END

-- ============================================
-- BILLING TABLE UPDATES
-- ============================================

-- Add missing columns to billing table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'billing' AND COLUMN_NAME = 'final_amount')
BEGIN
    ALTER TABLE billing ADD final_amount DECIMAL(10, 2) NULL;
    PRINT 'Added final_amount to billing table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'billing' AND COLUMN_NAME = 'payment_method')
BEGIN
    ALTER TABLE billing ADD payment_method NVARCHAR(50) NULL;
    PRINT 'Added payment_method to billing table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'billing' AND COLUMN_NAME = 'payment_status')
BEGIN
    ALTER TABLE billing ADD payment_status NVARCHAR(20) NULL;
    PRINT 'Added payment_status to billing table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'billing' AND COLUMN_NAME = 'notes')
BEGIN
    ALTER TABLE billing ADD notes NVARCHAR(1000) NULL;
    PRINT 'Added notes to billing table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'billing' AND COLUMN_NAME = 'created_at')
BEGIN
    ALTER TABLE billing ADD created_at DATETIME NULL;
    PRINT 'Added created_at to billing table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'billing' AND COLUMN_NAME = 'updated_at')
BEGIN
    ALTER TABLE billing ADD updated_at DATETIME NULL;
    PRINT 'Added updated_at to billing table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'billing' AND COLUMN_NAME = 'created_by')
BEGIN
    ALTER TABLE billing ADD created_by INT NULL;
    PRINT 'Added created_by to billing table';
END

-- ============================================
-- DEPARTMENTS TABLE UPDATES
-- ============================================

-- Add missing columns to departments table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'departments' AND COLUMN_NAME = 'location')
BEGIN
    ALTER TABLE departments ADD location NVARCHAR(200) NULL;
    PRINT 'Added location to departments table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'departments' AND COLUMN_NAME = 'phone')
BEGIN
    ALTER TABLE departments ADD phone NVARCHAR(20) NULL;
    PRINT 'Added phone to departments table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'departments' AND COLUMN_NAME = 'email')
BEGIN
    ALTER TABLE departments ADD email NVARCHAR(100) NULL;
    PRINT 'Added email to departments table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'departments' AND COLUMN_NAME = 'head_of_department')
BEGIN
    ALTER TABLE departments ADD head_of_department NVARCHAR(100) NULL;
    PRINT 'Added head_of_department to departments table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'departments' AND COLUMN_NAME = 'capacity')
BEGIN
    ALTER TABLE departments ADD capacity INT NULL;
    PRINT 'Added capacity to departments table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'departments' AND COLUMN_NAME = 'created_at')
BEGIN
    ALTER TABLE departments ADD created_at DATETIME NULL;
    PRINT 'Added created_at to departments table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'departments' AND COLUMN_NAME = 'updated_at')
BEGIN
    ALTER TABLE departments ADD updated_at DATETIME NULL;
    PRINT 'Added updated_at to departments table';
END

-- ============================================
-- USERS TABLE UPDATES (if needed)
-- ============================================

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'users' AND COLUMN_NAME = 'created_at')
BEGIN
    ALTER TABLE users ADD created_at DATETIME NULL;
    PRINT 'Added created_at to users table';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'users' AND COLUMN_NAME = 'updated_at')
BEGIN
    ALTER TABLE users ADD updated_at DATETIME NULL;
    PRINT 'Added updated_at to users table';
END

-- ============================================
-- CREATE INDEXES FOR BETTER PERFORMANCE
-- ============================================

-- Create indexes for frequently searched columns
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_doctors_department' AND object_id = OBJECT_ID('doctors'))
BEGIN
    CREATE INDEX IX_doctors_department ON doctors(department);
    PRINT 'Created index on doctors.department';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_appointments_patient_doctor' AND object_id = OBJECT_ID('appointments'))
BEGIN
    CREATE INDEX IX_appointments_patient_doctor ON appointments(patient_id, doctor_id);
    PRINT 'Created index on appointments.patient_id, doctor_id';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_appointments_date' AND object_id = OBJECT_ID('appointments'))
BEGIN
    CREATE INDEX IX_appointments_date ON appointments(appointment_date);
    PRINT 'Created index on appointments.appointment_date';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_billing_patient' AND object_id = OBJECT_ID('billing'))
BEGIN
    CREATE INDEX IX_billing_patient ON billing(patient_id);
    PRINT 'Created index on billing.patient_id';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_billing_status' AND object_id = OBJECT_ID('billing'))
BEGIN
    CREATE INDEX IX_billing_status ON billing(status);
    PRINT 'Created index on billing.status';
END

PRINT '===========================================';
PRINT 'Database schema updates completed successfully!';
PRINT '===========================================';
