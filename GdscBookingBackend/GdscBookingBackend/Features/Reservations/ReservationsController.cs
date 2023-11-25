using Microsoft.AspNetCore.Mvc;

namespace GdscBookingBackend.Features.Reservations;

[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    // private readonly IReservationsService _service;
    //
    // public ReservationsController(IReservationsService service)
    // {
    //     _service = service;
    // }
    //
    // [HttpGet]
    // public async Task<IActionResult> Get()
    // {
    //     var result = await _service.Get();
    //
    //     return Ok(result);
    // }
    //
    // [HttpGet("{id}")]
    // public async Task<IActionResult> Get(string id)
    // {
    //     var result = await _service.Get(id);
    //
    //     return Ok(result);
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> Post(Reservation model)
    // {
    //     var result = await _service.Create(model);
    //
    //     return Ok(result);
    // }
    //
    // [HttpPut("{id}")]
    // public async Task<IActionResult> Put(string id, Reservation model)
    // {
    //     var result = await _service.Update(id, model);
    //
    //     return Ok(result);
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(string id)
    // {
    //     var result = await _service.Delete(id);
    //
    //     return Ok(result);
    // }
}