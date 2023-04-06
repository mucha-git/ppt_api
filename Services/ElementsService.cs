namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models.Elements;
using WebApi.Models.Views;
using WebApi.Repositories;

public interface IElementsService
{
    Task<IEnumerable<Elements>> GetElements(int pilgrimageId, int year);
    Task<Elements> Create(CreateElementRequest request);
    Task<Elements> Update(UpdateElementRequest request);

    Task Delete(int id);
}

public class ElementsService : IElementsService
{
    private readonly IElementsRepository _elementsRepository;
    private readonly IElementsFactory _elementsFactory;
    private readonly IMapper _mapper;
    public ElementsService(IElementsRepository elementsRepository, IElementsFactory elementsFactory, IMapper mapper)
    {
        _elementsRepository = elementsRepository;
        _elementsFactory = elementsFactory;
        _mapper = mapper;
    }

    public async Task<Elements> Create(CreateElementRequest request)
    {
        var element = _elementsFactory.Create(request);
        if(element.mapHeight == 0) element.mapHeight = null;
        return await _elementsRepository.Create(element);
    }

    public async Task Delete(int id)
    {
        var element = await _elementsRepository.GetById(id);
        await _elementsRepository.Delete(element);
    }

    public async Task<IEnumerable<Elements>> GetElements(int pilgrimageId, int year)
    {
        return await _elementsRepository.Get(pilgrimageId, year);
    }

    public async Task<Elements> Update(UpdateElementRequest request)
    {
        var element = _mapper.Map<Elements>(request);
        if(element.mapHeight == 0) element.mapHeight = null;
        return await _elementsRepository.Update(element);
    }
}