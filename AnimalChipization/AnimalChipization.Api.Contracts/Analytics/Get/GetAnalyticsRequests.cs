using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Api.Contracts.Analytics.Get;

public class GetAnalyticsRequests
{
    [RegularExpression(@"\d\d\d\d-\d\d-\d\d", ErrorMessage = "Invalid date format")]
    public string StartDate { get; set; }

    [RegularExpression(@"\d\d\d\d-\d\d-\d\d", ErrorMessage = "Invalid date format")]
    public string EndDate { get; set; }
}