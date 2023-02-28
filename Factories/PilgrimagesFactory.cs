using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Pilgrimages;
using WebApi.Models.Views;

namespace WebApi.Factories
{
    public interface IPilgrimagesFactory
    {
        Pilgrimages Create(CreatePilgrimageRequest model);

    }

    public class PilgrimagesFactory : IPilgrimagesFactory
    {
        private readonly IMapper _mapper;

        public PilgrimagesFactory(
            IMapper mapper)
        {
            _mapper = mapper;
        }

        public Pilgrimages Create(CreatePilgrimageRequest model)
        {
            var response = _mapper.Map<Pilgrimages>(model);
            return response;
        }
    }
}