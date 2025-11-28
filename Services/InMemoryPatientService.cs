using IT_13FinalProject.Models;
using System.Collections.Concurrent;

namespace IT_13FinalProject.Services
{
    public class InMemoryPatientService : IPatientService
    {
        private readonly ConcurrentDictionary<int, Patient> _patients = new();
        private int _nextId = 1;

        public InMemoryPatientService()
        {
            // Add some sample patients for testing
            AddSamplePatients();
        }

        private void AddSamplePatients()
        {
            var samplePatients = new[]
            {
                new Patient { PatientId = _nextId++, FirstName = "Himeko", LastName = "Murata", Email = "himeko_murata@gmail.com", CreatedAt = DateTime.Now },
                new Patient { PatientId = _nextId++, FirstName = "Yae", LastName = "Miko", Email = "miko.yae@mail.com", CreatedAt = DateTime.Now },
                new Patient { PatientId = _nextId++, FirstName = "Black", LastName = "Swan", Email = "ms.blkswan@mail.com", CreatedAt = DateTime.Now }
            };

            foreach (var patient in samplePatients)
            {
                _patients[patient.PatientId] = patient;
            }
        }

        public Task<List<Patient>> GetAllAsync()
        {
            return Task.FromResult(_patients.Values.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToList());
        }

        public Task<Patient?> GetByIdAsync(int id)
        {
            _patients.TryGetValue(id, out var patient);
            return Task.FromResult(patient);
        }

        public Task<Patient> CreateAsync(Patient patient)
        {
            patient.PatientId = _nextId++;
            patient.CreatedAt = DateTime.Now;
            patient.UpdatedAt = DateTime.Now;
            _patients[patient.PatientId] = patient;
            return Task.FromResult(patient);
        }

        // ADD THESE TWO MISSING METHODS:
        public Task<Patient> UpdateAsync(Patient patient)
        {
            if (_patients.ContainsKey(patient.PatientId))
            {
                patient.UpdatedAt = DateTime.Now;
                _patients[patient.PatientId] = patient;
                return Task.FromResult(patient);
            }
            throw new ArgumentException($"Patient with ID {patient.PatientId} not found.");
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(_patients.TryRemove(id, out _));
        }
    }
}