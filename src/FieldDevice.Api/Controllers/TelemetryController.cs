using FieldDevice.Api.Data;
using FieldDevice.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FieldDevice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelemetryController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TelemetryController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TelemetryUpdate update)
        {
            var device = await _db.Devices.FindAsync(update.DeviceId);
            if (device == null) return NotFound();
            // lightweight: update status and last seen
            device.Status = update.Status;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }

    public record TelemetryUpdate(Guid DeviceId, DeviceStatus Status);
}
