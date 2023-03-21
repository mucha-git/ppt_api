using System.ComponentModel.DataAnnotations;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Models.Years;

public class CreateYearRequest {
    [Required]
    public int Year {get; set;}
    [Required]
    public string YearTopic { get; set; }
    [Required]
    public bool isActive {get; set;}

    public string ImgSrc { get; set; }
    [Required]
    public int PilgrimageId { get; set; }
}