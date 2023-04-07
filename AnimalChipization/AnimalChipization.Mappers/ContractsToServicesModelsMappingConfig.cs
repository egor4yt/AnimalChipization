using System.Globalization;
using AnimalChipization.Api.Contracts.Accounts.Search;
using AnimalChipization.Api.Contracts.Accounts.Update;
using AnimalChipization.Api.Contracts.Analytics.Get;
using AnimalChipization.Api.Contracts.Animals.ChangeAnimalTypeAnimals;
using AnimalChipization.Api.Contracts.Animals.Create;
using AnimalChipization.Api.Contracts.Animals.Search;
using AnimalChipization.Api.Contracts.Animals.Update;
using AnimalChipization.Api.Contracts.AnimalsTypes.Update;
using AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Get;
using AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Update;
using AnimalChipization.Api.Contracts.Areas.Update;
using AnimalChipization.Api.Contracts.Locations.Update;
using AnimalChipization.Services.Models.Account;
using AnimalChipization.Services.Models.Analytics;
using AnimalChipization.Services.Models.Animal;
using AnimalChipization.Services.Models.AnimalType;
using AnimalChipization.Services.Models.AnimalVisitedLocation;
using AnimalChipization.Services.Models.Area;
using AnimalChipization.Services.Models.Location;
using AutoMapper;
using NetTopologySuite.Geometries;

namespace AnimalChipization.Mappers;

public class ContractsToServicesModelsMappingConfig : Profile
{
    public ContractsToServicesModelsMappingConfig()
    {
        #region Accounts

        CreateMap<SearchAccountsRequests, SearchAccountModel>();
        CreateMap<UpdateAccountsRequest, UpdateAccountModel>();

        #endregion

        #region Locations

        CreateMap<UpdateLocationsRequest, UpdateLocationModel>();

        #endregion

        #region AnimalsTypes

        CreateMap<UpdateAnimalsTypesRequest, UpdateAnimalTypeModel>();

        #endregion

        #region Animals

        CreateMap<CreateAnimalsRequest, CreateAnimalModel>();
        CreateMap<SearchAnimalsRequests, SearchAnimalModel>();
        CreateMap<UpdateAnimalsRequest, UpdateAnimalModel>();
        CreateMap<ChangeAnimalTypeAnimalsRequest, ChangeAnimalTypeAnimalModel>();

        #endregion

        #region AnimalsVisitedLocations

        CreateMap<UpdateAnimalsVisitedLocationsRequest, UpdateAnimalVisitedLocationModel>();
        CreateMap<GetAnimalsVisitedLocationsRequest, GetAnimalVisitedLocationModel>();

        #endregion

        #region Areas

        CreateMap<UpdateAreasRequest, UpdateAreaModel>()
            .ForMember(x => x.AreaPoints, p => p.MapFrom(
                x =>
                    string.Join(
                        ";",
                        x.AreaPoints.Select(point => $"{point.Latitude.ToString(CultureInfo.InvariantCulture)},{point.Longitude.ToString(CultureInfo.InvariantCulture)}")
                    )));

        #endregion

        #region Analytics

        CreateMap<GetAnalyticsRequests, AnalyzeAnimalsMovementModel>()
            .ForMember(x => x.StartDate, p =>
                p.MapFrom<DateTime?>(x =>
                    string.IsNullOrWhiteSpace(x.StartDate)
                        ? DateTime.MinValue.ToUniversalTime()
                        : DateTime.ParseExact(x.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddDays(1).ToUniversalTime()))
            .ForMember(x => x.EndDate, p =>
                p.MapFrom<DateTime?>(x =>
                    string.IsNullOrWhiteSpace(x.EndDate)
                        ? DateTime.MaxValue.ToUniversalTime()
                        : DateTime.ParseExact(x.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddDays(1).ToUniversalTime()));

        #endregion
    }
}