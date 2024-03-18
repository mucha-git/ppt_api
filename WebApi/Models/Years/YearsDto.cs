using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.Elements;
using WebApi.Models.MapPins;
using WebApi.Models.Maps;
using WebApi.Models.Views;

namespace WebApi.Models.Years;

public class YearsDto
{
    public int Id { get; set; }
    public int Year {get; set;}

    public string YearTopic { get; set; }
    
    public bool isActive {get; set;}

    public string ImgSrc { get; set; }

    public Guid? Version { get; set; }
    
    public int ColumnsCount { get; set; }

    public int PilgrimageId { get; set; }

    public IList<ViewsDto> Views { get; set; }

    public IList<MapPinsDto> MapPins {get; set; }

    public IList<MapsDto> Maps {get; set; }

    public IList<ElementsDto> Elements {get; set; }

}