namespace AnimalChipization.Services.Models.Analytics;

public class AnalyzeAnimalsMovementResponse
{
    public long TotalQuantityAnimals { get; set; }
    public long TotalAnimalsArrived { get; set; }
    public long TotalAnimalsGone { get; set; }
    public List<AnalyzeAnimalsMovementResponseItem> AnimalsAnalytics { get; set; }
}