using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Models.Years;

public class CopyYearRequest {
    [Required]
    public int SourceYearId {get; set;}
    [Required]
    public int DestinationYearId { get; set; }
    [Required]
    public int PilgrimageId { get; set; }
}

public class CopyYearRequestValidator : AbstractValidator<CopyYearRequest> {
    public CopyYearRequestValidator(IValidations validations) {
        RuleFor( v => v).MustAsync(async (request , _) => {
            return await validations.IsValid(request.PilgrimageId, request.SourceYearId, request.DestinationYearId);
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 