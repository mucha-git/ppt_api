using System.ComponentModel.DataAnnotations.Schema;

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

    public IEnumerable<Markers> Markers { get; set; }
    public string Polylines { get; set; }
    //public IEnumerable<Coordinates> Polylines { get; set; }

    //public IEnumerable<Elements> Elements { get; set; }
    public int YearId { get; set; }
    public Years Year { get; set; }
}