using AnimalChipization.Api.Contracts.Accounts.GetById;
using AnimalChipization.Api.Contracts.Accounts.Search;
using AnimalChipization.Api.Contracts.Registration.Post;
using AnimalChipization.Data.Entities;
using AutoMapper;

namespace AnimalChipization.Mappers;

public class EntitiesToContractsMappingConfig : Profile
{
    public EntitiesToContractsMappingConfig()
    {
        CreateMap<Account, PostRegistrationResponse>();
        CreateMap<Account, GetByIdAccountsResponse>();
        CreateMap<Account, SearchAccountsResponse>();
    }
}