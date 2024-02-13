using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper.Configuration.Annotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Entities;

public class Maps
{
    public int Id { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string Provider { get; set; }
    [Column(TypeName = "varchar(250)")]
    public string Name { get; set; }

    public string MapSrc { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string StrokeColor { get; set; }
 
    public int StrokeWidth { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double? Delta { get; set; }
    [JsonIgnore]
    public string MarkersString { get; set; }
    [NotMapped]
    public IEnumerable<Markers> Markers { get { 
        var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };
        return JsonSerializer.Deserialize<IEnumerable<Markers>>(MarkersString, jsonOptions);
            } set { 
                var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };
                MarkersString = JsonSerializer.Serialize(value, jsonOptions);
            } }
    public string Polylines { get; set; }
    //public IEnumerable<Coordinates> Polylines { get; set; }

    //public IEnumerable<Elements> Elements { get; set; }
    public int YearId { get; set; }
    public Years Year { get; set; }
}