using Microsoft.AspNetCore.Mvc;
using zajecia_dot_net.DTOs;
using zajecia_dot_net.Models;
using zajecia_dot_net.Services;

namespace zajecia_dot_net.Controllers;

[ApiController]
[Route("api/sportsfacilities")]
public class SportsFacilitiesController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<SportsFacilityDto>> GetAll()
    {
        lock (InMemoryDataStore.SyncRoot)
        {
            return Ok(InMemoryDataStore.SportsFacilities.Select(ToDto).ToList());
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<SportsFacilityDto> GetById(int id)
    {
        lock (InMemoryDataStore.SyncRoot)
        {
            var facility = InMemoryDataStore.SportsFacilities.FirstOrDefault(item => item.Id == id);
            return facility is null ? NotFound("Nie znaleziono takiego obiektu sportowego.") : Ok(ToDto(facility));
        }
    }

    [HttpPost]
    public ActionResult<SportsFacilityDto> Create(CreateSportsFacilityDto dto)
    {
        lock (InMemoryDataStore.SyncRoot)
        {
            var facility = new SportsFacility
            {
                Id = InMemoryDataStore.SportsFacilities.Count == 0
                    ? 1
                    : InMemoryDataStore.SportsFacilities.Max(item => item.Id) + 1,
                Name = dto.Name.Trim(),
                SportType = dto.SportType.Trim(),
                Address = dto.Address.Trim(),
                HourlyRate = dto.HourlyRate
            };

            InMemoryDataStore.SportsFacilities.Add(facility);
            var result = ToDto(facility);

            return CreatedAtAction(nameof(GetById), new { id = facility.Id }, result);
        }
    }

    private static SportsFacilityDto ToDto(SportsFacility facility) => new()
    {
        Id = facility.Id,
        Name = facility.Name,
        SportType = facility.SportType,
        Address = facility.Address,
        HourlyRate = facility.HourlyRate
    };
}
