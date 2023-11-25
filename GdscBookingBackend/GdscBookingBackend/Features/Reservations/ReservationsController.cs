using GdscBookingBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscBookingBackend.Features.Reservations;

[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    
    public ReservationsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ReservationModel>> Post(ReservationRequest request)
    {
       var model = new ReservationModel
            {
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.UtcNow,
                Date = request.Date,
                Interval = request.Interval,
                UserId = request.UserId
            };
        
            var result = await _dbContext.Reservations.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            var uri = "/reservations/" + result.Entity.Id;
        
            return Created(uri, result.Entity);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ReservationView>> Get()
    {
        return Ok(await _dbContext
            .Reservations
            .Select(model => new ReservationView
            {
                Id = model.Id,
                Created = model.Created,
                Date = model.Date,
                Interval = model.Interval,
                UserId = model.UserId
            })
            .ToListAsync()
        );
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReservationView>> Get(string id)
    {
        var result = await _dbContext.Reservations.FirstOrDefaultAsync(e => e.Id == id);
        
        if(result == null)
        {
            return NotFound($"Reservation with id {id} not found.");
        }
    
        return Ok(new ReservationView
        {
            Id = result.Id,
            Created = result.Created,
            Date = result.Date,
            Interval = result.Interval,
            UserId = result.UserId
        });
    }
    
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ReservationView>> Put(string id, ReservationRequest request)
    {
        var oldModel = await _dbContext.Reservations.FirstOrDefaultAsync(e => e.Id == id);
        if (oldModel is null)
        {
            return NotFound($"Reservation with id {id} not found.");
        }
        
        var newModel = new ReservationModel
        {
            Id = oldModel.Id,
            Created = oldModel.Created,
            Date = request.Date == new DateOnly() ? oldModel.Date : request.Date,
            Interval = request.Interval == 0 ? oldModel.Interval : request.Interval,
            UserId = request.UserId.Equals(string.Empty) ? oldModel.UserId : request.UserId
        };
        
        var result = _dbContext.Reservations.Update(newModel);
        await _dbContext.SaveChangesAsync();

        return Ok(new ReservationView
        {
            Id = result.Entity.Id,
            Created = result.Entity.Created,
            Date = result.Entity.Date,
            Interval = result.Entity.Interval,
            UserId = result.Entity.UserId
        });
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ReservationView>> Delete(string id)
    {
        var model = await _dbContext.Reservations.FirstOrDefaultAsync(e => e.Id == id);
        if (model is null)
        {
            return NotFound($"Reservation with id {id} not found.");
        }
        
        var result = _dbContext.Reservations.Remove(model);
        await _dbContext.SaveChangesAsync();
    
        return Ok(new ReservationView
        {
            Id = result.Entity.Id,
            Created = result.Entity.Created,
            Date = result.Entity.Date,
            Interval = result.Entity.Interval,
            UserId = result.Entity.UserId
        });
    }
}