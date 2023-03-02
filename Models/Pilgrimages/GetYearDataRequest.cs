namespace WebApi.Models.Pilgrimages;

using System.ComponentModel.DataAnnotations;

public class GetYearDataRequest
{
    [Required]
    public int YearId {get; set; }
}