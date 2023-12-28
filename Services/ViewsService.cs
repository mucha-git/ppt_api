namespace WebApi.Services;

using AutoMapper;
using Microsoft.AspNetCore.Components;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models;
using WebApi.Models.Views;
using WebApi.Repositories;

public interface IViewsService
{
    Task<IEnumerable<Views>> GetViews(int yearId);
    Task<Views> Create(CreateViewRequest request);
    Task<Views> Update(UpdateViewRequest request);
    Task<List<ChangesResponse>> Copy(CopyViewsRequest request);
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

    public async Task<List<ChangesResponse>> Copy(CopyViewsRequest request)
    {
        var sourceViews = await _viewsRepository.Get(request.SourceYearId);
        var changes = await CopyRecursive(sourceViews.OrderBy(o => o.Id), new List<ChangesResponse>(), request.DestinationYearId, null);
        return changes;
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

    public async Task<IEnumerable<Views>> GetViews(int yearId)
    {
        return await _viewsRepository.Get(yearId);
    }

    public async Task<Views> Update(UpdateViewRequest request)
    {
        var view = _mapper.Map<Views>(request);
        return await _viewsRepository.Update(view);
    }

    private async Task<List<ChangesResponse>> CopyRecursive(IEnumerable<Views> sourceViews, List<ChangesResponse> changes, int destinationYearId, int? viewId){
        foreach (var view in sourceViews.Where(v => v.ViewId == viewId))
        {
            var sourceId = view.Id;
            var toCreate = _viewsFactory.Create(_mapper.Map<CreateViewRequest>(view));
            toCreate.YearId = destinationYearId;
            //view.Id = 0;
            if(toCreate.ViewId != null) {
                toCreate.ViewId = changes.First( v => v.SourceId == viewId).DestinationId;
                //view.View = null;
            }
            var newView = await _viewsRepository.Create(toCreate);
            changes.Add(new ChangesResponse{ SourceId = sourceId, DestinationId = newView.Id});
            changes = await CopyRecursive(sourceViews, changes, destinationYearId, sourceId);
        }
        return changes;
    }
}