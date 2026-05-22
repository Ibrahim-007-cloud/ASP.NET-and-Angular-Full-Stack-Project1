namespace ClinicManagement.Domain.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}