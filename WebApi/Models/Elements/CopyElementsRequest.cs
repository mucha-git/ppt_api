using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Elements;

public class CopyElementsRequest {
    [Required]
    public int SourceYearId {get; set;}
    [Required]
    public int DestinationYearId { get; set; }
    [Required]
    public List<ChangesResponse> MapsChanges { get; set; }
    [Required]
    public List<ChangesResponse> ViewsChanges { get; set; }
}