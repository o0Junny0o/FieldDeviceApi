using FieldDevice.Api.Data;
using FieldDevice.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FieldDevice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public DevicesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DeviceStatus? status, [FromQuery] string? location, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var query = _db.Devices.AsQueryable();
            if (status != null) query = query.Where(d => d.Status == status);
            if (!string.IsNullOrEmpty(location)) query = query.Where(d => d.Location == location);

            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(new { total, page, pageSize, items });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var d = await _db.Devices.FindAsync(id);
            if (d == null) return NotFound();
            return Ok(d);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Device device)
        {
            device.Id = Guid.NewGuid();
            _db.Devices.Add(device);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Device input)
        {
            var d = await _db.Devices.FindAsync(id);
            if (d == null) return NotFound();
            d.Model = input.Model;
            d.Location = input.Location;
            d.Status = input.Status;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var d = await _db.Devices.FindAsync(id);
            if (d == null) return NotFound();
            _db.Devices.Remove(d);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/incidents")]
        public async Task<IActionResult> CreateIncident(Guid id, Incident inc)
        {
            var d = await _db.Devices.FindAsync(id);
            if (d == null) return NotFound();
            inc.Id = Guid.NewGuid();
            inc.DeviceId = id;
            _db.Incidents.Add(inc);
            await _db.SaveChangesAsync();
            return CreatedAtAction(null, null);
        }
    }
}
