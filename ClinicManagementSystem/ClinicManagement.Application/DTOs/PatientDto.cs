using System;

namespace ClinicManagement.Application.DTOs
{
    public class PatientCreateDto
    {
        // No ID needed here (the database will auto-generate it)
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;

    }
}