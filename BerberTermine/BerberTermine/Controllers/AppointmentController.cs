using BarberTermine.Models;
using BerberTermine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberTermine.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly BarberDbContext _context;

    public AppointmentsController(BarberDbContext context)
    {
        _context = context;
    }

    // GET: api/appointments
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _context.Appointments.ToListAsync();
        return Ok(appointments);
    }

    // POST: api/appointments
    [HttpPost]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        // 1. Kontrollo nëse ora e saktë është zënë
        var sameTimeExists = await _context.Appointments.AnyAsync(a => a.Time == appointment.Time);
        if (sameTimeExists)
        {
            return BadRequest("Termini është i zënë!");
        }

        // 2. Kontrollo nëse i njëjti emër ka rezervuar tashmë në të njëjtën ditë
        var sameNameSameDay = await _context.Appointments.AnyAsync(a =>
            a.Name == appointment.Name && a.Time.Date == appointment.Time.Date);

        if (sameNameSameDay)
        {
            return BadRequest("Ju keni tashmë një termin me këtë emër për këtë ditë.");
        }

        // 3. Kontrollo nëse i njëjti numër telefoni ka rezervuar në të njëjtën ditë
        var samePhoneSameDay = await _context.Appointments.AnyAsync(a =>
            a.Phone == appointment.Phone && a.Time.Date == appointment.Time.Date);

        if (samePhoneSameDay)
        {
            return BadRequest("Ky numër telefoni ka tashmë një termin për këtë ditë.");
        }

        // 4. Ruaj terminin
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return Ok(appointment);
    }


    // DELETE: api/appointments/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return NotFound("Termini nuk u gjet!");
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
    {
        var filteredAppointments = await _context.Appointments
            .Where(a => a.Time.Date == date.Date)
            .ToListAsync();

        return Ok(filteredAppointments);
    }
}