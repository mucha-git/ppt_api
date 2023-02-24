using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Views;

namespace WebApi.Factories
{
    public interface IViewsFactory
    {
        Views Create(CreateViewRequest model);

    }

    public class ViewsFactory : IViewsFactory
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ViewsFactory(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Views Create(CreateViewRequest model)
        {
            var response = _mapper.Map<Views>(model);
            return response;
        }
    }
}