using AnimalChipization.Api.Contracts.Accounts.GetById;
using AnimalChipization.Api.Contracts.Accounts.Search;
using AnimalChipization.Api.Contracts.Accounts.Update;
using AnimalChipization.Api.Contracts.Locations.Create;
using AnimalChipization.Api.Contracts.Locations.GetById;
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

        #endregion
    }
}