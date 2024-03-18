namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models.Pilgrimages;
using WebApi.Repositories;

public interface IPilgrimagesService
{
    Task<IEnumerable<Pilgrimages>> GetPilgrimages(int? pilgrimageId);
    Task<Pilgrimages> GetPilgrimageForApp(int pilgrimageId);
    Task<Pilgrimages> Create(CreatePilgrimageRequest request);
    Task<Pilgrimages> Update(UpdatePilgrimageRequest request);

    Task Delete(int id);
}

public class PilgrimagesService : IPilgrimagesService
{
    private readonly IPilgrimagesRepository _pilgrimagesRepository;
    private readonly IPilgrimagesFactory _pilgrimagesFactory;
    private readonly IYearsService _yearsService;
    private readonly IMapper _mapper;
    public PilgrimagesService(IPilgrimagesRepository pilgrimagesRepository, 
    IPilgrimagesFactory pilgrimagesFactory,
    IYearsService yearsService, 
    IMapper mapper)
    {
        _pilgrimagesRepository = pilgrimagesRepository;
        _pilgrimagesFactory = pilgrimagesFactory;
        _yearsService = yearsService;
        _mapper = mapper;
    }

    public async Task<Pilgrimages> Create(CreatePilgrimageRequest request)
    {
        var pilgrimage = _pilgrimagesFactory.Create(request);
        Pilgrimages pilgrim = await _pilgrimagesRepository.Create(pilgrimage);
        Years year = await _yearsService.Create(new Models.Years.CreateYearRequest(){Year = DateTime.Now.Year, YearTopic = "", isActive = true, PilgrimageId = pilgrim.Id});
        pilgrim.Years.Append(year);
        return pilgrim;
    }

    public async Task Delete(int id)
    {
        var pilgrimage = await _pilgrimagesRepository.GetById(id);
        await _pilgrimagesRepository.Delete(pilgrimage);
    }

    public async Task<IEnumerable<Pilgrimages>> GetPilgrimages(int? pilgrimageId)
    {
        return await _pilgrimagesRepository.Get(pilgrimageId);
    }

    public async Task<Pilgrimages> GetPilgrimageForApp(int pilgrimageId)
    {
        return await _pilgrimagesRepository.GetPilgrimagesFromRedisById(pilgrimageId);
    }

    public async Task<Pilgrimages> Update(UpdatePilgrimageRequest request)
    {
        var pilgrimage = _mapper.Map<Pilgrimages>(request);
        return await _pilgrimagesRepository.Update(pilgrimage);
    }
}