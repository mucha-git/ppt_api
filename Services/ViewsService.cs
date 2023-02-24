namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models.Views;
using WebApi.Repositories;

public interface IViewsService
{
    Task<IEnumerable<Views>> GetViews(int pilgrimageId, int year);
    Task<Views> Create(CreateViewRequest request);
    Task<Views> Update(UpdateViewRequest request);

    Task Delete(int id);
}

public class ViewsService : IViewsService
{
    private readonly IViewsRepository _viewsRepository;
    private readonly IViewsFactory _viewsFactory;
    private readonly IMapper _mapper;
    public ViewsService(IViewsRepository viewsRepository, IViewsFactory viewsFactory, IMapper mapper)
    {
        _viewsRepository = viewsRepository;
        _viewsFactory = viewsFactory;
        _mapper = mapper;
    }

    public async Task<Views> Create(CreateViewRequest request)
    {
        var view = _viewsFactory.Create(request);
        return await _viewsRepository.Create(view);
    }

    public async Task Delete(int id)
    {
        var view = await _viewsRepository.GetById(id);
        await _viewsRepository.Delete(view);
    }

    public async Task<IEnumerable<Views>> GetViews(int pilgrimageId, int year)
    {
        return await _viewsRepository.Get(pilgrimageId, year);
    }

    public async Task<Views> Update(UpdateViewRequest request)
    {
        var view = _mapper.Map<Views>(request);
        return await _viewsRepository.Update(view);
    }
}