using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DrawController : ControllerBase
{
    private readonly IGroupDrawService _drawService;

    public DrawController(IGroupDrawService drawService)
    {
        _drawService = drawService;
    }

    [HttpPost]
    public async Task<IActionResult> RunDraw([FromBody] DrawRequestDto request)
    {
        if (request == null)
            return BadRequest("Request body cannot be null.");

        if (request.GroupCount != 4 && request.GroupCount != 8)
            return BadRequest("GroupCount must be 4 or 8.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _drawService.RunDrawAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
