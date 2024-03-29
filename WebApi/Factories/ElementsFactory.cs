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
            try {
var response = _mapper.Map<Elements>(model);
            //if(response.Text != null) response.Text = response.Text;//.Replace('\n', '');
            if(response.Order == null){
                var lastelement = _context.Elements.Where(v => v.YearId == model.YearId && v.ViewId == model.ViewId).OrderBy( o => o.Order).LastOrDefault();  
                response.Order = lastelement != null? lastelement.Order + 1 : 1;  
            }
            return response;
            } catch (Exception e) {
                throw new Exception(e.InnerException.Message);
            }
            
        }
    }
}