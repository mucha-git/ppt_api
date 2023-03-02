namespace WebApi.Models.Pilgrimages;

using System.ComponentModel.DataAnnotations;

public class GetYearDataRequest
{
    [Required]
    public int PilgrimageId { get; set; }
    [Required]
    public int YearId {get; set; }
}