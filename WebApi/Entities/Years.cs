using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Years
{
    public int Id { get; set; }
    public int Year {get; set;}

    [Column(TypeName = "varchar(500)")]
    public string YearTopic { get; set; }
    
    public bool isActive {get; set;}

    [Column(TypeName = "varchar(1000)")]
    public string ImgSrc { get; set; }

    public Guid? Version { get; set; }

    public int ColumnsCount { get; set; }
    public int PilgrimageId { get; set; }
    
    public Pilgrimages Pilgrimage { get; set; }

    public IEnumerable<Views> Views { get; set; }

    public IEnumerable<MapPins> MapPins {get; set; }

    public IEnumerable<Maps> Maps {get; set; }

    public IEnumerable<Elements> Elements {get; set; }

}