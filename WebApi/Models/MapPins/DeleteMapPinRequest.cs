using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Helpers;

namespace WebApi.Models.MapPins;

public class DeleteMapPinRequest {
    [Required]
    public int Id {get; set;}
    [Required]
    public int YearId {get; set;}
}

public class DeleteMapPinRequestValidator : AbstractValidator<DeleteMapPinRequest> {
    public DeleteMapPinRequestValidator(IValidations validations) {
        RuleFor( v => v.YearId).MustAsync(async (request , _) => {
            return await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v.Id).MustAsync(async (request , _) => {
            return await validations.IsMapPinValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 