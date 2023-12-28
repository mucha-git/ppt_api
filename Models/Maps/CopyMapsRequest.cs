using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Models.Maps;

public class CopyMapRequest {
    [Required]
    public int SourceYearId {get; set;}
    [Required]
    public int DestinationYearId { get; set; }
    [Required]
    public List<ChangesResponse> MapPinsChanges { get; set; }
}