using System.ComponentModel.DataAnnotations;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Models.Pilgrimages;

public class UpdatePilgrimageRequest {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public bool isActive {get; set;}

    public string LogoSrc { get; set; }
    public string OneSignal {get; set;}
}