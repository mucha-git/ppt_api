using System.ComponentModel.DataAnnotations;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Models.Maps;

public class UpdateMarkerRequest {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public string FooterText { get; set; }
    public string FooterColor { get; set; }
    public int StrokeWidth { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
    [Required]
    public int PinId { get; set; }
    [Required]
    public int MapId { get; set; }
}