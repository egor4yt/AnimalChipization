using AnimalChipization.Api.Contracts.Analytics.Get;
using AnimalChipization.Services.Models.Analytics;
using AutoMapper;

namespace AnimalChipization.Mappers;

public class ServicesModelsToContractsMappingConfig : Profile
{
    public ServicesModelsToContractsMappingConfig()
    {
        #region Analytics

        CreateMap<AnalyzeAnimalsMovementResponse, GetAnalyticsResponse>();
        CreateMap<AnalyzeAnimalsMovementResponseItem, GetAnalyticsResponseItem>();

        #endregion
    }
}