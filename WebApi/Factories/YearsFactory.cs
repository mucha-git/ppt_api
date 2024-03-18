using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Pilgrimages;
using WebApi.Models.Views;
using WebApi.Models.Years;

namespace WebApi.Factories
{
    public interface IYearsFactory
    {
        Years Create(CreateYearRequest model);

    }

    public class YearsFactory : IYearsFactory
    {
        private readonly IMapper _mapper;

        public YearsFactory(
            IMapper mapper)
        {
            _mapper = mapper;
        }

        public Years Create(CreateYearRequest model)
        {
            var response = _mapper.Map<Years>(model);
            return response;
        }
    }
}