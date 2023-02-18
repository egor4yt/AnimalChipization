using AnimalChipization.Api.Contracts.Accounts.Search;
using AnimalChipization.Api.Contracts.Accounts.Update;
using AnimalChipization.Api.Contracts.Animals.Create;
using AnimalChipization.Api.Contracts.Animals.Search;
using AnimalChipization.Api.Contracts.AnimalsTypes.Update;
using AnimalChipization.Api.Contracts.Locations.Update;
using AnimalChipization.Services.Models.Account;
using AnimalChipization.Services.Models.Animal;
using AnimalChipization.Services.Models.AnimalType;
using AnimalChipization.Services.Models.Location;
using AutoMapper;

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

        #endregion
    }
}