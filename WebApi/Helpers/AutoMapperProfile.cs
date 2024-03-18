namespace WebApi.Helpers;

using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Accounts;
using WebApi.Models.Elements;
using WebApi.Models.MapPins;
using WebApi.Models.Maps;
using WebApi.Models.Pilgrimages;
using WebApi.Models.Views;
using WebApi.Models.Years;

public class AutoMapperProfile : Profile
{
    // mappings between model and entity objects
    public AutoMapperProfile()
    {
        CreateMap<Account, AccountResponse>();

        CreateMap<Account, AuthenticateResponse>();

        CreateMap<RegisterRequest, Account>();

        CreateMap<CreateRequest, Account>();

        CreateMap<UpdateRequest, Account>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    // ignore null role
                    if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                    return true;
                }
            ));

        CreateMap<CreateViewRequest, Views>().ReverseMap();

        CreateMap<UpdateViewRequest, Views>();

        CreateMap<CreateElementRequest, Elements>().ReverseMap();

        CreateMap<UpdateElementRequest, Elements>();

        CreateMap<CreateMapRequest, Maps>();
        CreateMap<CreateCoordinateRequest, Coordinates>();
        CreateMap<CreateMarkerRequest, Markers>();
        CreateMap<CreatePilgrimageRequest, Pilgrimages>();
        CreateMap<CreateMapPinRequest, MapPins>();
        CreateMap<CreateYearRequest, Years>();
        CreateMap<CreateElementRequest, CreateViewRequest>().ForMember( dest => dest.Type, src => src.MapFrom(m => m.ViewType));

        CreateMap<UpdateMapRequest, Maps>();
        CreateMap<UpdateCoordinateRequest, Coordinates>();
        CreateMap<UpdateMarkerRequest, Markers>();
        CreateMap<UpdatePilgrimageRequest, Pilgrimages>();
        CreateMap<UpdateMapPinRequest, MapPins>();
        CreateMap<UpdateYearRequest, Years>();
        CreateMap<UpdateElementRequest, UpdateViewRequest>()
            .ForMember( dest => dest.Type, src => src.MapFrom(m => m.ViewType))
            .ForMember( dest => dest.Id, src => src.MapFrom( m => m.DestinationViewId));

        CreateMap<Years, YearsDto>();
        CreateMap<Views, ViewsDto>();
        CreateMap<Maps, MapsDto>();
        CreateMap<MapPins, MapPinsDto>();
        CreateMap<Elements, ElementsDto>();
    }
}