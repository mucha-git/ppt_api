using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.MapPins;

public class MapPinsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string PinSrc { get; set; }

    public string IconSrc { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public int YearId { get; set; }
}