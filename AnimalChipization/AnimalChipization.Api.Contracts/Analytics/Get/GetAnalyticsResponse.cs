namespace AnimalChipization.Api.Contracts.Analytics.Get;

public class GetAnalyticsResponse
{
    public long TotalQuantityAnimals { get; set; }
    public long TotalAnimalsArrived { get; set; }
    public long TotalAnimalsGone { get; set; }
    public List<GetAnalyticsResponseItem> AnimalsAnalytics { get; set; }
}