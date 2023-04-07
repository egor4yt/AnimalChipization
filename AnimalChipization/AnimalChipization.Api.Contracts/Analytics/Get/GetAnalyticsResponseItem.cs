namespace AnimalChipization.Api.Contracts.Analytics.Get;

public class GetAnalyticsResponseItem
{
    public string AnimalType { get; set; }
    public long AnimalTypeId { get; set; }
    public long QuantityAnimals { get; set; }
    public long AnimalsArrived { get; set; }
    public long AnimalsGone { get; set; }
}