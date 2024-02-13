using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class MapPins
{
    public int Id { get; set; }
    [Column(TypeName = "varchar(250)")]
    public string Name { get; set; }
    
    [Column(TypeName = "varchar(1000)")]
    public string PinSrc { get; set; }

    [Column(TypeName = "varchar(1000)")]
    public string IconSrc { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public int YearId { get; set; }
    public Years Year { get; set; }

    //public IEnumerable<Markers> Markers { get; set; }
}