namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models;
using WebApi.Models.Elements;
using WebApi.Models.Views;
using WebApi.Repositories;

public interface IElementsService
{
    Task<IEnumerable<Elements>> GetElements(int yearId);
    Task<Elements> Create(CreateElementRequest request);
    Task<Elements> Update(UpdateElementRequest request);
    Task<IEnumerable<Elements>> UpdateAll();
    Task Copy(CopyElementsRequest request);
    Task Delete(int id);
}

public class ElementsService : IElementsService
{
    private readonly IElementsRepository _elementsRepository;
    private readonly IViewsService _viewService;
    private readonly IElementsFactory _elementsFactory;
    private readonly IMapper _mapper;
    public ElementsService(
        IElementsRepository elementsRepository,
        IViewsService viewService,
        IElementsFactory elementsFactory,
        IMapper mapper)
    {
        _elementsRepository = elementsRepository;
        _viewService = viewService;
        _elementsFactory = elementsFactory;
        _mapper = mapper;
    }

    public async Task Copy(CopyElementsRequest request)
    {
        var changes = new List<ChangesResponse>();
        var sourceElements = await _elementsRepository.Get(request.SourceYearId);
        foreach (var element in sourceElements)
        {
            element.SetPropsValues();
            var toCreate = _elementsFactory.Create(_mapper.Map<CreateElementRequest>(element));
            toCreate.YearId = request.DestinationYearId;
            toCreate.ViewId = request.ViewsChanges.First(v => v.SourceId == toCreate.ViewId).DestinationId;
            switch (toCreate.Type)
            {
                case Helpers.ElementType.Map: 
                    toCreate.MapId = request.MapsChanges.First(m => m.SourceId == toCreate.MapId).DestinationId;
                    toCreate.Map = null;
                    break;
                case Helpers.ElementType.Navigation:
                    toCreate.DestinationViewId = request.ViewsChanges.First(v => v.SourceId == toCreate.DestinationViewId).DestinationId;
                    break;
                case Helpers.ElementType.View:
                    toCreate.DestinationViewId = request.ViewsChanges.First(v => v.SourceId == toCreate.DestinationViewId).DestinationId;
                    break;
            }
            toCreate.SetValues();
            await _elementsRepository.Create(toCreate);
        }
    }

    public async Task<Elements> Create(CreateElementRequest request)
    {
        if (request.Type == Helpers.ElementType.View)
        {
            CreateViewRequest createViewRequest = _mapper.Map<CreateViewRequest>(request);
            Views view = await _viewService.Create(createViewRequest);
            request.DestinationViewId = view.Id;
        }
        var element = _elementsFactory.Create(request);
        if (element.MapHeight == 0) element.MapHeight = null;
        element.SetValues();
        return await _elementsRepository.Create(element);
    }

    public async Task Delete(int id)
    {
        var element = await _elementsRepository.GetById(id);
        await _elementsRepository.Delete(element);
        if(element.Type == Helpers.ElementType.View){
            await _viewService.Delete((int)element.DestinationViewId);
        }
    }

    public async Task<IEnumerable<Elements>> GetElements(int yearId)
    {
        var ev = await _elementsRepository.Get(yearId);
    foreach (var item in ev)
    {
        item.SetPropsValues();
    }
        return ev;
    }

    public async Task<Elements> Update(UpdateElementRequest request)
    {
        if (request.Type == Helpers.ElementType.View)
        {
            UpdateViewRequest updateViewRequest = _mapper.Map<UpdateViewRequest>(request);
            Views view = await _viewService.Update(updateViewRequest);
            request.DestinationViewId = view.Id;
        }
        var element = _mapper.Map<Elements>(request);
        if (element.MapHeight == 0) element.MapHeight = null;
        element.SetValues();
        return await _elementsRepository.Update(element);
    }

    public async Task<IEnumerable<Elements>> UpdateAll()
    {
        var elements = await _elementsRepository.UpdateAll();
        return elements;
    }
}