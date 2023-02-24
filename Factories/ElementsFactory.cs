using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Elements;
using WebApi.Models.Views;

namespace WebApi.Factories
{
    public interface IElementsFactory
    {
        Elements Create(CreateElementRequest model);

    }

    public class ElementsFactory : IElementsFactory
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ElementsFactory(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Elements Create(CreateElementRequest model)
        {
            var response = _mapper.Map<Elements>(model);
            if(response.Text != null) response.Text = response.Text.Replace('\n', 'n');
            return response;
        }
    }
}