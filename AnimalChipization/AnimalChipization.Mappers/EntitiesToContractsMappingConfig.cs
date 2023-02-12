using AnimalChipization.Api.Contracts.Account.GetById;
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
    }
}