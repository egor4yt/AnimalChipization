using AnimalChipization.Api.Contracts.Accounts.GetById;
using AnimalChipization.Api.Contracts.Accounts.Search;
using AnimalChipization.Api.Contracts.Accounts.Update;
using AnimalChipization.Api.Contracts.AnimalsTypes.Post;
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
        CreateMap<Account, SearchAccountsResponse>();
        CreateMap<Account, UpdateAccountsResponse>();

        #endregion

        #region Location

        CreateMap<Location, GetByIdLocationsResponse>();
        CreateMap<Location, CreateLocationsResponse>();
        CreateMap<Location, UpdateLocationsResponse>();

        #endregion

        #region AnimalType

        CreateMap<AnimalType, PostAnimalsTypesResponse>();

        #endregion
    }
}