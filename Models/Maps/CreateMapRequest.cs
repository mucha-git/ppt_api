using System.ComponentModel.DataAnnotations;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Models.Maps;

public class CreateMapRequest {
    [Required]
    public string Provider { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string MapSrc { get; set; }
    [Required]
    public string StrokeColor { get; set; }
    [Required]
    public int StrokeWidth { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
    [Required]
    public double? Delta { get; set; }
    public IEnumerable<CreateMarkerRequest> Markers { get; set; }
    public string Polylines {get; set;}
    //public IEnumerable<CreateCoordinateRequest> Polylines { get; set; }
    [Required]
    public int YearId { get; set; }
}