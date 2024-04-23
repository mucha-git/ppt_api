using System.ComponentModel.DataAnnotations;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Years;

namespace WebApi.Models.Pilgrimages;

public class CreatePilgrimageRequest {
    [Required]
    public string Name { get; set; }
    [Required]
    public bool isActive {get; set;}

    public string LogoSrc { get; set; }
    public int? GroupId { get; set; }
    public List<CreateYearRequest> Years {get; set;}
}