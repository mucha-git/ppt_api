using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.MapPins;
using WebApi.Models.Maps;
using WebApi.Models.Views;

namespace WebApi.Factories
{
    public interface IMapPinsFactory
    {
        MapPins Create(CreateMapPinRequest model);

    }

    public class MapPinsFactory : IMapPinsFactory
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MapPinsFactory(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public MapPins Create(CreateMapPinRequest model)
        {
            var response = _mapper.Map<MapPins>(model);
            return response;
        }
    }
}