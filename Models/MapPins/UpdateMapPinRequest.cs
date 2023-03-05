using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.MapPins;

public class UpdateMapPinRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string PinSrc { get; set; }
    public string IconSrc { get; set; }
    [Required]
    public int Width { get; set; }
    [Required]
    public int Height { get; set; }
    [Required]
    public int YearId { get; set; }
}