using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Models.Views;

public class CopyViewsRequest {
    [Required]
    public int SourceYearId {get; set;}
    [Required]
    public int DestinationYearId { get; set; }
}