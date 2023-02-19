using AnimalChipization.Api.Contracts.Accounts.GetById;
using AnimalChipization.Api.Contracts.Accounts.Search;
using AnimalChipization.Api.Contracts.Accounts.Update;
using AnimalChipization.Api.Contracts.Animals.AttachAnimalType;
using AnimalChipization.Api.Contracts.Animals.ChangeAnimalTypeAnimals;
using AnimalChipization.Api.Contracts.Animals.Create;
using AnimalChipization.Api.Contracts.Animals.DeleteAnimalType;
using AnimalChipization.Api.Contracts.Animals.GetById;
using AnimalChipization.Api.Contracts.Animals.Search;
using AnimalChipization.Api.Contracts.Animals.Update;
using AnimalChipization.Api.Contracts.AnimalsTypes.Create;
using AnimalChipization.Api.Contracts.AnimalsTypes.GetById;
using AnimalChipization.Api.Contracts.AnimalsTypes.Update;
using AnimalChipization.Api.Contracts.AnimalsVisitedLocations;
using AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Add;
using AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Update;
using AnimalChipization.Api.Contracts.Locations.Create;
using AnimalChipization.Api.Contracts.Locations.GetById;
using AnimalChipization.Api.Contracts.Locations.Update;
using AnimalChipization.Api.Contracts.Registration.Post;
using AnimalChipization.Data.Entities;
using AutoMapper;

namespace AnimalChipization.Mappers;

public class EntitiesToContractsMappingConfig : Profile
{
    public EntitiesToContractsMappingConfig()
    {
        #region Account

        CreateMap<Account, PostRegistrationResponse>();
        CreateMap<Account, GetByIdAccountsResponse>();
        CreateMap<Account, SearchAccountsResponseItem>();
        CreateMap<Account, UpdateAccountsResponse>();

        #endregion

        #region Location

        CreateMap<Location, GetByIdLocationsResponse>();
        CreateMap<Location, CreateLocationsResponse>();
        CreateMap<Location, UpdateLocationsResponse>();

        #endregion

        #region AnimalType

        CreateMap<AnimalType, CreateAnimalsTypesResponse>();
        CreateMap<AnimalType, GetByIdAnimalsTypesResponse>();
        CreateMap<AnimalType, UpdateAnimalsTypesResponse>();

        #endregion

        #region Animals

        CreateMap<Animal, CreateAnimalsResponse>()
            .ForMember(x => x.AnimalTypes, opt => opt.MapFrom(p => p.AnimalTypes.Select(x => x.Id)))
            .ForMember(x => x.Weight, opt => opt.MapFrom(p => p.WeightKilograms))
            .ForMember(x => x.Height, opt => opt.MapFrom(p => p.HeightMeters))
            .ForMember(x => x.Length, opt => opt.MapFrom(p => p.LengthMeters))
            .ForMember(x => x.VisitedLocations, opt => opt.MapFrom(p => p.AnimalVisitedLocations.Select(x => x.Id)));

        CreateMap<Animal, GetByIdAnimalsResponse>()
            .ForMember(x => x.AnimalTypes, opt => opt.MapFrom(p => p.AnimalTypes.Select(x => x.Id)))
            .ForMember(x => x.Weight, opt => opt.MapFrom(p => p.WeightKilograms))
            .ForMember(x => x.Height, opt => opt.MapFrom(p => p.HeightMeters))
            .ForMember(x => x.Length, opt => opt.MapFrom(p => p.LengthMeters))
            .ForMember(x => x.VisitedLocations, opt => opt.MapFrom(p => p.AnimalVisitedLocations.Select(x => x.Id)));

        CreateMap<Animal, SearchAnimalsResponseItem>()
            .ForMember(x => x.AnimalTypes, opt => opt.MapFrom(p => p.AnimalTypes.Select(x => x.Id)))
            .ForMember(x => x.Weight, opt => opt.MapFrom(p => p.WeightKilograms))
            .ForMember(x => x.Height, opt => opt.MapFrom(p => p.HeightMeters))
            .ForMember(x => x.Length, opt => opt.MapFrom(p => p.LengthMeters))
            .ForMember(x => x.VisitedLocations, opt => opt.MapFrom(p => p.AnimalVisitedLocations.Select(x => x.Id)));

        CreateMap<Animal, UpdateAnimalsResponse>()
            .ForMember(x => x.AnimalTypes, opt => opt.MapFrom(p => p.AnimalTypes.Select(x => x.Id)))
            .ForMember(x => x.Weight, opt => opt.MapFrom(p => p.WeightKilograms))
            .ForMember(x => x.Height, opt => opt.MapFrom(p => p.HeightMeters))
            .ForMember(x => x.Length, opt => opt.MapFrom(p => p.LengthMeters))
            .ForMember(x => x.VisitedLocations, opt => opt.MapFrom(p => p.AnimalVisitedLocations.Select(x => x.Id)));

        CreateMap<Animal, AttachAnimalTypeAnimalsResponse>()
            .ForMember(x => x.AnimalTypes, opt => opt.MapFrom(p => p.AnimalTypes.Select(x => x.Id)))
            .ForMember(x => x.Weight, opt => opt.MapFrom(p => p.WeightKilograms))
            .ForMember(x => x.Height, opt => opt.MapFrom(p => p.HeightMeters))
            .ForMember(x => x.Length, opt => opt.MapFrom(p => p.LengthMeters))
            .ForMember(x => x.VisitedLocations, opt => opt.MapFrom(p => p.AnimalVisitedLocations.Select(x => x.Id)));

        CreateMap<Animal, DeleteAnimalTypeAnimalsResponse>()
            .ForMember(x => x.AnimalTypes, opt => opt.MapFrom(p => p.AnimalTypes.Select(x => x.Id)))
            .ForMember(x => x.Weight, opt => opt.MapFrom(p => p.WeightKilograms))
            .ForMember(x => x.Height, opt => opt.MapFrom(p => p.HeightMeters))
            .ForMember(x => x.Length, opt => opt.MapFrom(p => p.LengthMeters))
            .ForMember(x => x.VisitedLocations, opt => opt.MapFrom(p => p.AnimalVisitedLocations.Select(x => x.Id)));

        CreateMap<Animal, ChangeAnimalTypeAnimalsResponse>()
            .ForMember(x => x.AnimalTypes, opt => opt.MapFrom(p => p.AnimalTypes.Select(x => x.Id)))
            .ForMember(x => x.Weight, opt => opt.MapFrom(p => p.WeightKilograms))
            .ForMember(x => x.Height, opt => opt.MapFrom(p => p.HeightMeters))
            .ForMember(x => x.Length, opt => opt.MapFrom(p => p.LengthMeters))
            .ForMember(x => x.VisitedLocations, opt => opt.MapFrom(p => p.AnimalVisitedLocations.Select(x => x.Id)));

        #endregion

        #region AnimalVisitedLocation

        CreateMap<AnimalVisitedLocation, AddAnimalsVisitedLocationsResponse>()
            .ForMember(x => x.DateTimeOfVisitLocationPoint, opt => opt.MapFrom(p => p.CreatedAt))
            .ForMember(x => x.LocationPointId, opt => opt.MapFrom(p => p.LocationId));

        CreateMap<AnimalVisitedLocation, UpdateAnimalsVisitedLocationsResponse>()
            .ForMember(x => x.DateTimeOfVisitLocationPoint, opt => opt.MapFrom(p => p.CreatedAt))
            .ForMember(x => x.LocationPointId, opt => opt.MapFrom(p => p.LocationId));

        #endregion
    }
}