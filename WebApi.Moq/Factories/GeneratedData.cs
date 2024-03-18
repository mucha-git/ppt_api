using System.Text.Json;
using Bogus;
using Bogus.Locations;
using WebApi.Entities;

namespace WebApi.Moq.Factories;

public class GeneratedData
{
    private Faker<Maps> mapsModelFaker;
    //Microsoft Campus in Redmond, WA.
    double centerLat = 47.640320;
    double centerLon = -122.134400;

    //3 mile radius in meters:
    double radiusMeters = 4828.03;

    public GeneratedData()
    {
        Randomizer.Seed = new Random(12345);
        
        

        MapPins mapPinsModelFaker = new Faker<MapPins>()
            .RuleFor(x => x.Id, y => y.Random.Int(1))
            .RuleFor(x => x.Name, y => y.Address.City())
            .RuleFor(x => x.YearId, 1)
            .RuleFor(x => x.Height, y => y.Random.Int(1, 100))
            .RuleFor(x => x.Width, y => y.Random.Int(1, 100))
            .RuleFor(x => x.IconSrc, y => y.Internet.Url())
            .RuleFor(x => x.PinSrc, y => y.Internet.Url()).Generate();

        Markers markersModelFaker = new Faker<Markers>()
            .RuleFor(x => x.Id, y => y.Random.Int(1))
            .RuleFor(x => x.StrokeWidth, y => y.Random.Double(1, 5))
            .RuleFor(x => x.Latitude, y => y.Location().AreaCircle(centerLat, centerLon, radiusMeters).Latitude)
            .RuleFor(x => x.Longitude, y => y.Location().AreaCircle(centerLat, centerLon, radiusMeters).Longitude)
            .RuleFor(x => x.Description, y => y.Name.JobDescriptor())
            .RuleFor(x => x.Title, y => y.Name.JobTitle())
            .RuleFor(x => x.FooterColor, y => y.Commerce.Color())
            .RuleFor(x => x.PinId, y => mapPinsModelFaker.Id)
            .RuleFor(x => x.FooterText, y => y.Name.FullName()).Generate();


        mapsModelFaker = new Faker<Maps>()
            .RuleFor(x => x.Id, y => y.Random.Int(1))
            .RuleFor(x => x.Delta, y => y.Random.Double())
            .RuleFor(x => x.Latitude, y => y.Location().AreaCircle(centerLat, centerLon, radiusMeters).Latitude)
            .RuleFor(x => x.Longitude, y => y.Location().AreaCircle(centerLat, centerLon, radiusMeters).Longitude)
            .RuleFor(x => x.StrokeWidth, y => y.Random.Int(1, 5))
            .RuleFor(x => x.YearId, y => 1)
            .RuleFor(x => x.Name, y => y.Name.FirstName())
            .RuleFor(x => x.Polylines, y => "")
            .RuleFor(x => x.Provider, y => "google")
            .RuleFor(x => x.MapSrc, y => y.Internet.Url())
            .RuleFor(x => x.MarkersString, y => JsonSerializer.Serialize(markersModelFaker))
            .RuleFor(x => x.Markers, y => new List<Markers> {markersModelFaker})
            .RuleFor(x => x.StrokeColor, y => y.Commerce.Color());
    }

    public Maps GenerateMaps()
    {
        return mapsModelFaker.Generate();
    }
}