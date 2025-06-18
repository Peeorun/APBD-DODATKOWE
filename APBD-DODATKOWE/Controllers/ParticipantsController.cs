using APBD_DODATKOWE.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_DODATKOWE.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ParticipantsController : ControllerBase
{
    private readonly IParticipantService _participantService;

    public ParticipantsController(IParticipantService participantService)
    {
        _participantService = participantService;
    }

    [HttpGet("{participantId}/report")]
    public async Task<IActionResult> GetParticipantReport(int participantId)
    {
        var result = await _participantService.GetParticipantReportAsync(participantId);
            
        if (!result.Success)
        {
            return NotFound(result.Message);
        }

        return Ok(result.Report);
    }
}