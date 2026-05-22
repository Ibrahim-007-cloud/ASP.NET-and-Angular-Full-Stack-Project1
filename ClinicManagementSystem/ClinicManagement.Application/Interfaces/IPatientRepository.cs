using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync(string? searchTerm);
        Task<Patient?> GetByIdAsync(int id);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Doctor>> GetDoctorsAsync();
        Task AddVisitAsync(Visit visit);
    }
}