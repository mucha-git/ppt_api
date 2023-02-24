using System.ComponentModel.DataAnnotations;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Models.Maps;

public class CreateCoordinateRequest {
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
}