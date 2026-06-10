using Microsoft.AspNetCore.Mvc;
using SmartStay.Application.Dto.BookingDto;
using SmartStay.Application.Interfaces;

namespace SmartStay.API.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingRequestDto dto)
    {
        var result = await _bookingService.CreateBookingAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _bookingService.GetBookingAsync(id);
        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId)
    {
        var result = await _bookingService.GetUserBookingsAsync(userId);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelBookingRequestDto dto)
    {
        var result = await _bookingService.CancelBookingAsync(id, dto);
        return Ok(result);
    }
}
