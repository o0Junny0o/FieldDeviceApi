using System.ComponentModel.DataAnnotations;

namespace FieldDevice.Api.Models
{
    public class Incident
    {
        [Key]
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Open";
    }
}
