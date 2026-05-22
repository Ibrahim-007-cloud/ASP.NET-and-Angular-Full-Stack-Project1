namespace ClinicManagement.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}