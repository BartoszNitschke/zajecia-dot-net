using Microsoft.AspNetCore.Mvc;
using zajecia_dot_net.DTOs;
using zajecia_dot_net.Models;
using zajecia_dot_net.Services;

namespace zajecia_dot_net.Controllers;

[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<ReservationDto>> GetAll()
    {
        lock (InMemoryDataStore.SyncRoot)
        {
            return Ok(InMemoryDataStore.Reservations.Select(ToDto).ToList());
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<ReservationDto> GetById(int id)
    {
        lock (InMemoryDataStore.SyncRoot)
        {
            var reservation = InMemoryDataStore.Reservations.FirstOrDefault(item => item.Id == id);
            return reservation is null ? NotFound("Nie znaleziono takiej rezerwacji.") : Ok(ToDto(reservation));
        }
    }

    [HttpPost]
    public ActionResult<ReservationDto> Create(CreateReservationDto dto)
    {
        if (dto.StartTime == default || dto.EndTime == default || dto.EndTime <= dto.StartTime)
        {
            return BadRequest(new { message = "Termin zakończenia musi być późniejszy niż termin rozpoczęcia." });
        }

        lock (InMemoryDataStore.SyncRoot)
        {
            var facility = InMemoryDataStore.SportsFacilities
                .FirstOrDefault(item => item.Id == dto.SportsFacilityId);

            if (facility is null)
            {
                return BadRequest(new { message = "Wskazany obiekt sportowy nie istnieje." });
            }

            var overlaps = InMemoryDataStore.Reservations.Any(existing =>
                existing.SportsFacilityId == dto.SportsFacilityId &&
                dto.StartTime < existing.EndTime && dto.EndTime > existing.StartTime);

            if (overlaps)
            {
                return Conflict(new { message = "Obiekt jest już zarezerwowany w podanym terminie." });
            }

            var reservation = new Reservation
            {
                Id = InMemoryDataStore.Reservations.Count == 0
                    ? 1
                    : InMemoryDataStore.Reservations.Max(item => item.Id) + 1,
                SportsFacilityId = facility.Id,
                CustomerName = dto.CustomerName.Trim(),
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            InMemoryDataStore.Reservations.Add(reservation);
            var result = ToDto(reservation);

            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, result);
        }
    }

    private static ReservationDto ToDto(Reservation reservation)
    {
        var facility = InMemoryDataStore.SportsFacilities
            .First(item => item.Id == reservation.SportsFacilityId);

        return new ReservationDto
        {
            Id = reservation.Id,
            SportsFacilityId = reservation.SportsFacilityId,
            SportsFacilityName = facility.Name,
            CustomerName = reservation.CustomerName,
            StartTime = reservation.StartTime,
            EndTime = reservation.EndTime
        };
    }
}
