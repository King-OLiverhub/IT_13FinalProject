using IT_13FinalProject.Models;
using System.Collections.Concurrent;

namespace IT_13FinalProject.Services
{
    public class InMemoryHealthRecordService : IHealthRecordService
    {
        private readonly ConcurrentDictionary<int, HealthRecord> _records = new();
        private int _nextId = 1;

        public Task<List<HealthRecord>> GetAllAsync()
        {
            return Task.FromResult(_records.Values.OrderByDescending(r => r.RecordDate).ToList());
        }

        public Task<HealthRecord?> GetByIdAsync(int id)
        {
            _records.TryGetValue(id, out var record);
            return Task.FromResult(record);
        }

        public Task<List<HealthRecord>> GetByPatientIdAsync(int patientId)
        {
            var records = _records.Values
                .Where(r => r.PatientId == patientId)
                .OrderByDescending(r => r.RecordDate)
                .ToList();
            return Task.FromResult(records);
        }

        public Task<List<HealthRecord>> GetByDoctorIdAsync(int doctorId)
        {
            var records = _records.Values
                .Where(r => r.DoctorId == doctorId)
                .OrderByDescending(r => r.RecordDate)
                .ToList();
            return Task.FromResult(records);
        }

        public Task<HealthRecord> CreateAsync(HealthRecord record)
        {
            record.RecordId = _nextId++;
            record.RecordDate ??= DateTime.Now;
            _records[record.RecordId] = record;
            return Task.FromResult(record);
        }

        public Task<HealthRecord> UpdateAsync(HealthRecord record)
        {
            if (_records.ContainsKey(record.RecordId))
            {
                _records[record.RecordId] = record;
                return Task.FromResult(record);
            }
            throw new ArgumentException($"Health record with ID {record.RecordId} not found.");
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(_records.TryRemove(id, out _));
        }

        public Task<List<HealthRecord>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllAsync();

            var term = searchTerm.ToLower();
            var results = _records.Values
                .Where(r =>
                    (r.Diagnosis?.ToLower().Contains(term) == true) ||
                    (r.ChiefComplaint?.ToLower().Contains(term) == true) ||
                    (r.Icd10Code?.ToLower().Contains(term) == true))
                .OrderByDescending(r => r.RecordDate)
                .ToList();

            return Task.FromResult(results);
        }
    }
}