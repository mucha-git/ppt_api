namespace WebApi.Models.Pilgrimages;

using System.ComponentModel.DataAnnotations;

public class GetPilgrimageRequest
{
    [Required]
    public int PilgrimageId { get; set; }
}