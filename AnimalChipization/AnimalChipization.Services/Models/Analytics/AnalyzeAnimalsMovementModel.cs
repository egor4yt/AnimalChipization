namespace AnimalChipization.Services.Models.Analytics;

public class AnalyzeAnimalsMovementModel
{
    public long AreaId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}