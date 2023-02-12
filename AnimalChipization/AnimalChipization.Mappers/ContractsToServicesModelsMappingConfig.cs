using AnimalChipization.Api.Contracts.Accounts.Search;
using AnimalChipization.Api.Contracts.Accounts.Update;
using AnimalChipization.Api.Contracts.Locations.Update;
using AnimalChipization.Services.Models.Account;
using AnimalChipization.Services.Models.Location;
using AutoMapper;

namespace AnimalChipization.Mappers;

public class ContractsToServicesModelsMappingConfig : Profile
{
    public ContractsToServicesModelsMappingConfig()
    {
        #region Accounts
        
        CreateMap<SearchAccountsRequests, SearchAccountModel>();
        CreateMap<UpdateAccountsRequest, UpdateAccountModel>()
            .ForMember(x=>x.Email, opt=>opt.MapFrom(p=>p.Email.ToLower().Trim()));
        
        #endregion
        
        #region Locations
        
        CreateMap<UpdateLocationsRequest, UpdateLocationModel>();
        
        #endregion
        
    }
}