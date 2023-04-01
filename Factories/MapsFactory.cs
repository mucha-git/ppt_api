using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Maps;

namespace WebApi.Factories
{
    public interface IMapsFactory
    {
        Maps Create(CreateMapRequest model);

    }

    public class MapsFactory : IMapsFactory
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MapsFactory(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Maps Create(CreateMapRequest model)
        {
            var response = _mapper.Map<Maps>(model);
            return response;
        }
    }
}