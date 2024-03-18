using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class ChangesResponse {
    [Required]
    public int SourceId {get; set;}
    [Required]
    public int DestinationId { get; set; }
}