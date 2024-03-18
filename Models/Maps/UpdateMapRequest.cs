using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Models.Maps;

public class UpdateMapRequest {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Provider { get; set; }
    [Required]
    public string MapSrc { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string StrokeColor { get; set; }
    [Required]
    public int StrokeWidth { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
    [Required]
    public double? Delta { get; set; }
    public IEnumerable<UpdateMarkerRequest> Markers { get; set; }
    public string Polylines {get; set;}
    //public IEnumerable<CreateCoordinateRequest> Polylines { get; set; }
    [Required]
    public int YearId { get; set; }
}

public class UpdateMapRequestValidator : AbstractValidator<UpdateMapRequest> {
    public UpdateMapRequestValidator(IValidations validations) {
        RuleFor( v => v.YearId).MustAsync(async (request , _) => {
            return await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v.Id).MustAsync(async (request , _) => {
            return await validations.IsMapValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 