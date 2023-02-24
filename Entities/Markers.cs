using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApi.Entities;

public class Markers
{
    public int Id { get; set; }
    
    [Column(TypeName = "varchar(100)")]
    public string Title { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string Description { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string FooterText { get; set; }

    [Column(TypeName = "varchar(10)")]
    public string FooterColor { get; set; }

    public int StrokeWidth { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public int PinId { get; set; }
    //public MapPins Pin { get; set; }

    /*public int MapId { get; set; }
    public Maps Map { get; set; }*/
}