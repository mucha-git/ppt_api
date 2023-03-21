using System.ComponentModel.DataAnnotations;
using WebApi.Helpers;

namespace WebApi.Models.Years;

public class UpdateYearRequest {
    [Required]
     public int Id { get; set; }
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