using IT_13FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT_13FinalProject.Services
{
    public interface IHealthRecordService
    {
        Task<List<HealthRecord>> GetAllAsync();
        Task<HealthRecord?> GetByIdAsync(int id);
        Task<List<HealthRecord>> GetByPatientIdAsync(int patientId);
        Task<List<HealthRecord>> GetByDoctorIdAsync(int doctorId);
        Task<HealthRecord> CreateAsync(HealthRecord record);
        Task<HealthRecord> UpdateAsync(HealthRecord record);
        Task<bool> DeleteAsync(int id);
        Task<List<HealthRecord>> SearchAsync(string searchTerm);
    }

    public class HealthRecordService : IHealthRecordService
    {
        private readonly ApplicationDbContext _context;

        public HealthRecordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HealthRecord>> GetAllAsync()
        {
            try
            {
                return await _context.HealthRecords
                    .Include(hr => hr.Patient)
                    .Include(hr => hr.Doctor)
                    .Include(hr => hr.Nurse)
                    .OrderByDescending(hr => hr.RecordDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all health records: {ex.Message}");
                return new List<HealthRecord>();
            }
        }

        public async Task<HealthRecord?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.HealthRecords
                    .Include(hr => hr.Patient)
                    .Include(hr => hr.Doctor)
                    .Include(hr => hr.Nurse)
                    .FirstOrDefaultAsync(hr => hr.RecordId == id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting health record by ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<HealthRecord>> GetByPatientIdAsync(int patientId)
        {
            try
            {
                return await _context.HealthRecords
                    .Include(hr => hr.Patient)
                    .Include(hr => hr.Doctor)
                    .Include(hr => hr.Nurse)
                    .Where(hr => hr.PatientId == patientId)
                    .OrderByDescending(hr => hr.RecordDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting health records for patient {patientId}: {ex.Message}");
                return new List<HealthRecord>();
            }
        }

        public async Task<List<HealthRecord>> GetByDoctorIdAsync(int doctorId)
        {
            try
            {
                return await _context.HealthRecords
                    .Include(hr => hr.Patient)
                    .Include(hr => hr.Doctor)
                    .Include(hr => hr.Nurse)
                    .Where(hr => hr.DoctorId == doctorId)
                    .OrderByDescending(hr => hr.RecordDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting health records for doctor {doctorId}: {ex.Message}");
                return new List<HealthRecord>();
            }
        }

        public async Task<HealthRecord> CreateAsync(HealthRecord record)
        {
            try
            {
                if (record.RecordDate == null)
                    record.RecordDate = DateTime.Now;

                if (string.IsNullOrEmpty(record.VisitStatus))
                    record.VisitStatus = "Pending Assessment";

                _context.HealthRecords.Add(record);
                await _context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating health record: {ex.Message}");
                throw;
            }
        }

        public async Task<HealthRecord> UpdateAsync(HealthRecord record)
        {
            try
            {
                var existingRecord = await _context.HealthRecords
                    .FirstOrDefaultAsync(hr => hr.RecordId == record.RecordId);

                if (existingRecord is null)
                    throw new ArgumentException($"Health record with ID {record.RecordId} not found.");

                _context.Entry(existingRecord).CurrentValues.SetValues(record);
                await _context.SaveChangesAsync();
                return existingRecord;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating health record {record.RecordId}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var record = await _context.HealthRecords
                    .FirstOrDefaultAsync(hr => hr.RecordId == id);

                if (record != null)
                {
                    _context.HealthRecords.Remove(record);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting health record {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<List<HealthRecord>> SearchAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return await GetAllAsync();

                var termLower = searchTerm.Trim().ToLower();

                // Build EF-translatable predicate without null-propagating operators
                return await _context.HealthRecords
                    .Include(hr => hr.Patient)
                    .Include(hr => hr.Doctor)
                    .Include(hr => hr.Nurse)
                    .Where(hr =>
                        (hr.Patient != null && hr.Patient.FirstName != null && hr.Patient.FirstName.ToLower().Contains(termLower)) ||
                        (hr.Patient != null && hr.Patient.LastName != null && hr.Patient.LastName.ToLower().Contains(termLower)) ||
                        (hr.Doctor != null && hr.Doctor.FirstName != null && hr.Doctor.FirstName.ToLower().Contains(termLower)) ||
                        (hr.Doctor != null && hr.Doctor.LastName != null && hr.Doctor.LastName.ToLower().Contains(termLower)) ||
                        (hr.Diagnosis != null && hr.Diagnosis.ToLower().Contains(termLower)) ||
                        (hr.ChiefComplaint != null && hr.ChiefComplaint.ToLower().Contains(termLower)) ||
                        (hr.Icd10Code != null && hr.Icd10Code.ToLower().Contains(termLower))
                    )
                    .OrderByDescending(hr => hr.RecordDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching health records: {ex.Message}");
                return new List<HealthRecord>();
            }
        }
    }
}