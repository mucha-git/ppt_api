using AutoMapper;
using Moq;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Models.Maps;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Moq.Services;

public class MapsServiceTests
{
    private readonly MapsService _mapsService;
    private readonly Mock<IMapsRepository> _mapsRepositoryMock = new Mock<IMapsRepository>();
    private readonly Mock<IMapsFactory> _mapsFactoryMock = new Mock<IMapsFactory>();
    private readonly IMapper _mapper;

    public MapsServiceTests()
    {
        _mapper = new MapperConfiguration(mc =>
                                      {
                                          //mc.CreateMap<UpdateMapRequest, Maps>().ReverseMap();
                                          //mc.CreateMap<UpdateMarkerRequest, Markers>().ReverseMap();
                                          mc.AddProfile<AutoMapperProfile>();
                                      }).CreateMapper();
        _mapsService = new MapsService(_mapsRepositoryMock.Object, _mapsFactoryMock.Object, _mapper);
    }


    [Fact]
    public async Task GetMapById_ShouldReturnMap_WhenMapExist()
    {
        // Arrange
        var mapId = 1;
        var mapSrc = "mapSrc";
        var longitude = 0.0;
        var latitude = 0.0;
        var polylines = "[]";
        var strokeWidth = 1;
        var strokeColor = "#fff";
        var markers = new List<Markers>();
        var markersString = "[]";
        var delta = 1;
        var yearId = 1;
        var name = "name";
        var provider = "google";
        var year = new Years();
        var mapResult = new Maps {
            Id = mapId, 
            MapSrc = mapSrc, 
            Longitude = longitude, 
            Latitude = latitude, 
            Polylines = polylines, 
            StrokeWidth = strokeWidth, 
            StrokeColor = strokeColor, 
            Markers = markers, 
            MarkersString = markersString, 
            Delta = delta, 
            YearId = yearId, 
            Name = name, 
            Provider = provider,
            Year = year
        };
        _mapsRepositoryMock.Setup(x => x.GetById(mapId)).ReturnsAsync(mapResult);
        
        // Act
        var map = await _mapsService.GetMapById(mapId);

        // Assert
        Assert.Equal(mapId, map.Id);
        Assert.Equal(mapSrc, mapResult.MapSrc);
        Assert.Equal(longitude, mapResult.Longitude);
        Assert.Equal(latitude, mapResult.Latitude);
        Assert.Equal(polylines, mapResult.Polylines);
        Assert.Equal(strokeWidth, mapResult.StrokeWidth);
        Assert.Equal(strokeColor, mapResult.StrokeColor);
        Assert.Equal(markers, mapResult.Markers);
        Assert.Equal(markersString, mapResult.MarkersString);
        Assert.Equal(delta, mapResult.Delta);
        Assert.Equal(yearId, mapResult.YearId);
        Assert.Equal(name, mapResult.Name);
        Assert.Equal(provider, mapResult.Provider);
        Assert.Equal(year, mapResult.Year);
    }

    [Fact]
    public async Task Create_ShouldReturnMap_WithGivenData()
    {
        // Arrange
        var mapSrc = "mapSrc";
        var longitude = 0.0;
        var latitude = 0.0;
        var polylines = "[]";
        var strokeWidth = 1;
        var strokeColor = "#fff";
        var markersForRequest = new List<CreateMarkerRequest>();
        var delta = 1;
        var yearId = 1;
        var name = "name";
        var provider = "google";
        var mapRequest = new CreateMapRequest {
            MapSrc = mapSrc, 
            Longitude = longitude, 
            Latitude = latitude, 
            Polylines = polylines, 
            StrokeWidth = strokeWidth, 
            StrokeColor = strokeColor, 
            Markers = markersForRequest,
            Delta = delta, 
            YearId = yearId, 
            Name = name, 
            Provider = provider
        };
        var mapsFactoryResponse = _mapper.Map<Maps>(mapRequest);
        _mapsFactoryMock.Setup(x => x.Create(mapRequest)).Returns(mapsFactoryResponse);
        _mapsRepositoryMock.Setup(x => x.Create(mapsFactoryResponse)).ReturnsAsync(mapsFactoryResponse);
        // Act
        var map = await _mapsService.Create(mapRequest);

        // Assert
        Assert.Equal(map.MapSrc, mapRequest.MapSrc);
        Assert.Equal(map.Longitude, mapRequest.Longitude);
        Assert.Equal(map.Latitude, mapRequest.Latitude);
        Assert.Equal(map.Polylines, mapRequest.Polylines);
        Assert.Equal(map.StrokeWidth, mapRequest.StrokeWidth);
        Assert.Equal(map.StrokeColor, mapRequest.StrokeColor);
        Assert.Equal(map.Markers, _mapper.Map<IEnumerable<Markers>>(mapRequest.Markers));
        Assert.Equal(map.Delta, mapRequest.Delta);
        Assert.Equal(map.YearId, mapRequest.YearId);
        Assert.Equal(map.Name, mapRequest.Name);
        Assert.Equal(map.Provider, mapRequest.Provider);
        Assert.Equal(map.Id, mapsFactoryResponse.Id);
    }

    [Fact]
    public async Task GetMaps_ShouldReturnListOfMaps_WithGivenYearId()
    {
        // Arrange
        var yearId = 1;
        var mapId = 1;
        var mapSrc = "mapSrc";
        var longitude = 0.0;
        var latitude = 0.0;
        var polylines = "[]";
        var strokeWidth = 1;
        var strokeColor = "#fff";
        var markers = new List<Markers>();
        var markersString = "[]";
        var delta = 1;
        var name = "name";
        var provider = "google";
        var year = new Years();
        var mapResult = new Maps {
            Id = mapId, 
            MapSrc = mapSrc, 
            Longitude = longitude, 
            Latitude = latitude, 
            Polylines = polylines, 
            StrokeWidth = strokeWidth, 
            StrokeColor = strokeColor, 
            Markers = markers, 
            MarkersString = markersString, 
            Delta = delta, 
            YearId = yearId, 
            Name = name, 
            Provider = provider,
            Year = year
        };
        var mapList = new List<Maps>();
        mapList.Add(mapResult);
        _mapsRepositoryMock.Setup(x => x.Get(yearId)).ReturnsAsync(mapList);
        
        // Act
        var maps = await _mapsService.GetMaps(yearId);
        // Assert
        Assert.All(maps, map => Assert.Equal(map.YearId, yearId));
    }

    [Fact]
    public async Task Update_ShouldReturnUpdatedMap_WithGivenModel()
    {
        // Arrange
        var yearId = 1;
        var mapId = 1;
        var mapSrc = "mapSrc";
        var longitude = 0.0;
        var latitude = 0.0;
        var polylines = "[]";
        var strokeWidth = 1;
        var strokeColor = "#fff";
        var requestMarkers = new List<UpdateMarkerRequest>();
        var delta = 1;
        var name = "name";
        var provider = "google";
        var year = new Years();
        var updateMapRequest = new UpdateMapRequest
        {
            Id = mapId,
            MapSrc = mapSrc,
            Longitude = longitude,
            Latitude = latitude,
            Polylines = polylines,
            StrokeWidth = strokeWidth,
            StrokeColor = strokeColor,
            Markers = requestMarkers,
            Delta = delta,
            YearId = yearId,
            Name = name,
            Provider = provider
        };

        var map = _mapper.Map<Maps>(updateMapRequest);
        _mapsFactoryMock.Setup(x => x.Update(updateMapRequest)).Returns(map);
        _mapsRepositoryMock.Setup(x => x.Update(map)).ReturnsAsync(map);
        
        // Act
        var result = await _mapsService.Update(updateMapRequest);

        // Assert
        _mapsRepositoryMock.Verify( x => x.Update(map), Times.Once);
        Assert.Equal(mapId, result.Id);
    }

    [Fact]
    public async Task Delete_ShouldRemoveMap_WithGivenMap()
    {
        // Arrange
        var mapId = 1;
        var mapSrc = "mapSrc";
        var longitude = 0.0;
        var latitude = 0.0;
        var polylines = "[]";
        var strokeWidth = 1;
        var strokeColor = "#fff";
        var markers = new List<Markers>();
        var markersString = "[]";
        var delta = 1;
        var yearId = 1;
        var name = "name";
        var provider = "google";
        var year = new Years();
        var mapResult = new Maps {
            Id = mapId, 
            MapSrc = mapSrc, 
            Longitude = longitude, 
            Latitude = latitude, 
            Polylines = polylines, 
            StrokeWidth = strokeWidth, 
            StrokeColor = strokeColor, 
            Markers = markers, 
            MarkersString = markersString, 
            Delta = delta, 
            YearId = yearId, 
            Name = name, 
            Provider = provider,
            Year = year
        };
        _mapsRepositoryMock.Setup(x => x.Delete(mapResult));
        _mapsRepositoryMock.Setup(x => x.GetById(mapId)).ReturnsAsync(mapResult);
        
        // Act
        await _mapsService.Delete(mapId);

        // Assert
        _mapsRepositoryMock.Verify( x => x.Delete(mapResult), Times.Once);
    }

    [Fact]
    public async Task Copy_ShouldCopyWholeYearData_WithGivenData()
    {
        // Arrange
        var mapChange = new ChangesResponse {SourceId = 0, DestinationId = 2};
        var changes = new List<ChangesResponse>() {mapChange};
        
        var mapPinsChange = new ChangesResponse {SourceId = 1, DestinationId = 2};
        var copyMapRequest = new CopyMapRequest
        {
            SourceYearId = 1,
            DestinationYearId = 2,
            MapPinsChanges = new List<ChangesResponse> {mapPinsChange}
        };
        var sourceMarker = new CreateMarkerRequest
                {
                    Description = "description", 
                    Longitude = 0, 
                    Latitude = 0, 
                    StrokeWidth = 0, 
                    FooterColor = "#fff",
                    FooterText = "text", 
                    Id = 1, 
                    PinId = 1, 
                    Title = "title"
                };
        var mapSrc = "mapSrc";
        var longitude = 0.0;
        var latitude = 0.0;
        var polylines = "[]";
        var strokeWidth = 1;
        var strokeColor = "#fff";
        var markersForRequest = new List<CreateMarkerRequest>(){sourceMarker};
        var delta = 1;
        var name = "name";
        var provider = "google";

        
        var sourceMap = new CreateMapRequest {
            MapSrc = mapSrc, 
            Longitude = longitude, 
            Latitude = latitude, 
            Polylines = polylines, 
            StrokeWidth = strokeWidth, 
            StrokeColor = strokeColor, 
            Markers = markersForRequest,
            Delta = delta, 
            YearId = copyMapRequest.SourceYearId, 
            Name = name, 
            Provider = provider
        };
        var sourceMapFactoryResponse = _mapper.Map<Maps>(sourceMap);
        
        _mapsFactoryMock.Setup(x => x.Create(sourceMap)).Returns(sourceMapFactoryResponse);
        var mapsList = new List<Maps> {sourceMapFactoryResponse};
        _mapsRepositoryMock.Setup(x => x.Get(copyMapRequest.SourceYearId)).ReturnsAsync(mapsList);
        
        var destinationMarker = new CreateMarkerRequest
        {
            Description = "description", 
            Longitude = 0, 
            Latitude = 0, 
            StrokeWidth = 0, 
            FooterColor = "#fff",
            FooterText = "text", 
            Id = 1, 
            PinId = 2, 
            Title = "title"
        };
        
        var mapRequest = new CreateMapRequest {
            MapSrc = mapSrc, 
            Longitude = longitude, 
            Latitude = latitude, 
            Polylines = polylines, 
            StrokeWidth = strokeWidth, 
            StrokeColor = strokeColor, 
            Markers = markersForRequest,
            Delta = delta, 
            YearId = copyMapRequest.DestinationYearId, 
            Name = name, 
            Provider = provider
        };
        var destinationMapFactoryResponse = _mapper.Map<Maps>(mapRequest);
        destinationMapFactoryResponse.Id = 2;
        _mapsFactoryMock.Setup(x => x.Create(It.IsAny<CreateMapRequest>())).Returns(destinationMapFactoryResponse);
        _mapsRepositoryMock.Setup(x => x.Create(destinationMapFactoryResponse))
            .ReturnsAsync(destinationMapFactoryResponse);
        
        // Act
        var response = await _mapsService.Copy(copyMapRequest);

        // Assert
        Assert.Equal(response.Count, changes.Count);
        Assert.Collection(response, item => changes.Contains(item));
    }
}
