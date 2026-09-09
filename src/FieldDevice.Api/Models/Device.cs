using System.ComponentModel.DataAnnotations;

namespace FieldDevice.Api.Models
{
    public enum DeviceStatus { Active, Maintenance, Inactive }

    public class Device
n    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string SerialNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public DeviceStatus Status { get; set; } = DeviceStatus.Active;
        public string Location { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
