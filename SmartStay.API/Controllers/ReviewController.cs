using Microsoft.AspNetCore.Mvc;
using SmartStay.Application.Interfaces;

namespace SmartStay.API.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("room/{roomId:guid}")]
    public async Task<IActionResult> GetByRoom(Guid roomId)
    {   
        var result = await _reviewService.GetAllByRoomIdAsync(roomId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewRequestDto dto)
    {
        var result = await _reviewService.CreateReviewAsync(dto);
        return CreatedAtAction(nameof(GetByRoom), new { roomId = result.RoomId }, result);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReviewRequestDto dto)
    {
        var result = await _reviewService.UpdateReviewAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _reviewService.DeleteReviewAsync(id);
        return NoContent();
    }
}