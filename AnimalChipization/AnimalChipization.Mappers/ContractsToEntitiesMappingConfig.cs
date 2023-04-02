using AnimalChipization.Api.Contracts.Accounts.Create;
using AnimalChipization.Api.Contracts.AnimalsTypes.Create;
using AnimalChipization.Api.Contracts.Areas.Create;
using AnimalChipization.Api.Contracts.Locations.Create;
using AnimalChipization.Api.Contracts.Registration.Post;
using AnimalChipization.Core.Helpers;
using AnimalChipization.Data.Entities;
using AutoMapper;
using NetTopologySuite.Geometries;
using Location = AnimalChipization.Data.Entities.Location;

namespace AnimalChipization.Mappers;

public class ContractsToEntitiesMappingConfig : Profile
{
    public ContractsToEntitiesMappingConfig()
    {
        #region Registration

        CreateMap<PostRegistrationRequest, Account>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(p => SecurityHelper.ComputeSha256Hash(p.Password)))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(p => p.Email.ToLower().Trim()));

        #endregion

        #region Accounts

        CreateMap<CreateAccountsRequest, Account>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(p => SecurityHelper.ComputeSha256Hash(p.Password)))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(p => p.Email.ToLower().Trim()));
        ;

        #endregion

        #region Locations

        CreateMap<CreateLocationsRequest, Location>();

        #endregion

        #region AnimalsTypes

        CreateMap<CreateAnimalsTypesRequest, AnimalType>();

        #endregion

        #region Areas

        CreateMap<CreateAreasRequest, Area>()
            .ForMember(x=>x.AreaPoints, p=>p.Ignore());

        #endregion
    }
}