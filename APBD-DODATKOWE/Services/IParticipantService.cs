using APBD_DODATKOWE.DTOs;

namespace APBD_DODATKOWE.Services;

public interface IParticipantService
{
    Task<(bool Success, string Message, ParticipantReportDto? Report)> GetParticipantReportAsync(int participantId);
}