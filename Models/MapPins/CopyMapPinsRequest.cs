using System.ComponentModel.DataAnnotations;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Models.MapPins;

public class CopyMapPinsRequest {
    [Required]
    public int SourceYearId {get; set;}
    [Required]
    public int DestinationYearId { get; set; }
}