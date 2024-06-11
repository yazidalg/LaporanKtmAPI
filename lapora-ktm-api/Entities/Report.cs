using System.Text.Json.Serialization;

namespace lapora_ktm_api.Entities
{
    public class Report
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string StudentId { get; set; }
        public Student? Student { get; set; }
    }
}