using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ClinicDbContext _context;

        public PatientRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync(string? searchTerm)
        {
            var query = _context.Patients
                .Include(p => p.Visits)
                .ThenInclude(v => v.Doctor)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(searchTerm) || 
                                         p.Visits.Any(v => v.Doctor!.Name.ToLower().Contains(searchTerm)));
            }

            return await query.ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.Visits)
                .ThenInclude(v => v.Doctor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Patient patient) => await _context.Patients.AddAsync(patient);
        
        public async Task UpdateAsync(Patient patient) => _context.Patients.Update(patient);

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }
        }

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<IEnumerable<Doctor>> GetDoctorsAsync() => await _context.Doctors.ToListAsync();

        public async Task AddVisitAsync(Visit visit) => await _context.Visits.AddAsync(visit);
    }
}