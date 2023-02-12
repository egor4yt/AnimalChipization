using AnimalChipization.Api.Contracts.Accounts.Search;
using AnimalChipization.Services.Models.Account;
using AutoMapper;

namespace AnimalChipization.Mappers;

public class ContractsToServicesModelsMappingConfig : Profile
{
    public ContractsToServicesModelsMappingConfig()
    {
        CreateMap<SearchAccountsRequests, SearchAccountModel>();
    }
}