using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Entities;

namespace WebApi.Models.Maps;

public class MapsDto
{
    public int Id { get; set; }
    
    public string Provider { get; set; }
    public string Name { get; set; }

    public string MapSrc { get; set; }

    public string StrokeColor { get; set; }
 
    public int StrokeWidth { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double? Delta { get; set; }
    
    public IEnumerable<Markers> Markers { get; set;  }
    public string Polylines { get; set; }
    
    public int YearId { get; set; }
}