namespace WebApi.Models.Pilgrimages;

using System.ComponentModel.DataAnnotations;

public class GetDataRequest
{
    [Required]
    public int PilgrimageId { get; set; }
    [Required]
    public int Year {get; set; }
}