using AnimalChipization.Services.Models.Analytics;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAnalyticsService
{
    Task<AnalyzeAnimalsMovementResponse> AnalyzeAnimalsMovement(AnalyzeAnimalsMovementModel model);
}